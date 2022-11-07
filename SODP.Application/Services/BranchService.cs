using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Helpers;
using SODP.Domain.Services;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Branch> _validator;
        private readonly SODPDBContext _context;

        public BranchService(IMapper mapper, IValidator<Branch> validator, SODPDBContext context)
        {
            _mapper = mapper;
            _validator = validator;
            _context = context;
        }

        public async Task<ServicePageResponse<BranchDTO>> GetAllAsync()
        {
            return await GetAllAsync(null);
        }

        public async Task<ServicePageResponse<BranchDTO>> GetAllAsync(bool? activeOnly = null)
        {
            var serviceResponse = new ServicePageResponse<BranchDTO>();

            try
            {
                var branches = _context.Branches
                    .Where(x => (activeOnly == null || !activeOnly.Value) || (x.ActiveStatus == activeOnly))
                    .OrderBy(x => x.Sign);

                serviceResponse.Data.TotalCount = await branches.CountAsync();

                var br = await branches.ToListAsync();

                //serviceResponse.Data.PageNumber = currentPage;
                //serviceResponse.Data.PageSize = pageSize;
                serviceResponse.SetData(_mapper.Map<IList<BranchDTO>>(branches));

            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<BranchDTO>> GetAsync(int id)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == id);
                if(branch == null)
                {
                    serviceResponse.SetError($"Błąd: Branża Id:{id} nie odnaleziona.", 401);
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

        public async Task<ServiceResponse<BranchDTO>> GetAsync(string sign)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Sign == sign);
                if (branch == null)
                {
                    serviceResponse.SetError($"Błąd: Branża {sign} nie odnaleziona.", 401);
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

        public async Task<ServiceResponse<BranchDTO>> CreateAsync(BranchDTO newBranch)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Sign == newBranch.Sign);
                if(branch != null)
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
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id != updateBranch.Id && x.Sign.Equals(updateBranch.Sign));
                if(branch != null)
                {
                    serviceResponse.SetError("Branża już istnieje", 409);
                    return serviceResponse;
                }

                branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == updateBranch.Id);
                if (branch == null)
                {
                    serviceResponse.SetError($"Branża {updateBranch.Id} nie odnaleziona.", 404);
                    serviceResponse.ValidationErrors.Add("Sign", "Branża nie odnaleziona.");
                    return serviceResponse;
                }
                branch.Symbol = updateBranch.Symbol;
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

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == id);
                if(branch == null)
                {
                    serviceResponse.SetError($"Błąd: Branża Id:{id} nie odnaleziona.", 401);
                    return serviceResponse;
                }

                //var projectBranch = await _context.ProjectBranches.FirstOrDefaultAsync(x => x.BranchId == id);
                //if(projectBranch != null)
                //{
                //    serviceResponse.SetError($"Błąd: Branża {projectBranch.Branch.Sign} posiada powiązane projekty.", 400);
                //    return serviceResponse;
                //}

                _context.Entry(branch).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> SetActiveStatusAsync(int id, bool status)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == id);

                if (branch == null)
                {
                    serviceResponse.SetError($"Branża Id:{id} nie odnaleziona.", 404);

                    return serviceResponse;
                }

                branch.ActiveStatus = status;
                _context.Branches.Update(branch);
                await _context.SaveChangesAsync();
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
    }
}
