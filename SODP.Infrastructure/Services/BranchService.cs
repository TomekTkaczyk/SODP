using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Domain.Helpers;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Security.Cryptography.X509Certificates;

namespace SODP.Infrastructure.Services
{
    public class BranchService : FilteredPageService<Branch, BranchDTO>, IBranchService
    {

		public BranchService(IMapper mapper, IValidator<Branch> validator, SODPDBContext context, IActiveStatusService<Branch> activeStatusService) : base(mapper, validator, context, activeStatusService) { }


        public async Task<ServiceResponse<BranchDTO>> CreateAsync(BranchDTO newBranch)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.SingleOrDefaultAsync(x => x.Sign == newBranch.Sign);
                if (branch != null)
                {
                    serviceResponse.SetError($"Błąd: Branża {newBranch.Sign} już istnieje.", 400);
                    return serviceResponse;
                }

                branch = _mapper.Map<Branch>(newBranch);
                var validationResult = await _validator.ValidateAsync(branch);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    return serviceResponse;
                }

                branch.Normalize();
                branch.ActiveStatus = true;
                var entity = await _context.Branches.AddAsync(branch);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<BranchDTO>(entity.Entity));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse> UpdateAsync(BranchDTO updateBranch)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var branch = await _context.Branches.SingleOrDefaultAsync(x => x.Id != updateBranch.Id && x.Sign.Equals(updateBranch.Sign));
                if (branch != null)
                {
                    serviceResponse.SetError("Branża już istnieje", 409);
                    return serviceResponse;
                }

                branch = await _context.Branches.SingleOrDefaultAsync(x => x.Id == updateBranch.Id);
                if (branch == null)
                {
                    serviceResponse.SetError($"Branża {updateBranch.Id} nie odnaleziona.", 404);
                    serviceResponse.ValidationErrors.Add("Sign", "Branża nie odnaleziona.");
                    return serviceResponse;
                }
                branch.Sign = updateBranch.Sign;
                branch.Name = updateBranch.Name;
                branch.Normalize();
                _context.Branches.Update(branch);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<BranchDTO>> GetAsync(string sign)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.SingleOrDefaultAsync(x => x.Sign == sign);
                if (branch == null)
                {
                    serviceResponse.SetError($"Error: Branch {sign} not found.", 404);
                    return serviceResponse;
                }
                serviceResponse.SetData(_mapper.Map<BranchDTO>(branch));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }


        public async Task<ServicePageResponse<LicenseDTO>> GetLicensesAsync(int id)
        {
            var serviceResponse = new ServicePageResponse<LicenseDTO>();

            try
            {
                var branch = await _context.BranchLicenses
                    .Include(x => x.License)
                    .ThenInclude(x => x.Designer)
                    .Where(k => k.BranchId == id)
                    .ToListAsync();

                serviceResponse.SetData(_mapper.Map<IList<LicenseDTO>>(branch.Select(x => x.License)));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public override async Task<ServicePageResponse<BranchDTO>> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var serviceResponse = new ServicePageResponse<BranchDTO>();

            var pageCollection = _query
                .Where(x => !active.HasValue || x.ActiveStatus.Value.Equals(active))
                .Where(x => x.Sign.Contains(searchString) || x.Name.Contains(searchString))
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            serviceResponse.SetData(_mapper.Map<IList<BranchDTO>>(await pageCollection.ToListAsync()));

            return serviceResponse;
        }


    }
}
