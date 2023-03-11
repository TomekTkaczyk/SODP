using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Domain.Helpers;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
{
    public class LicenseService : FilteredPageService<License, LicenseDTO>, ILicenseService
    {
        public LicenseService(IMapper mapper, IValidator<License> validator, SODPDBContext context, IActiveStatusService<License> activeStatusService) : base(mapper, validator, context, activeStatusService) { }

        public async Task<ServiceResponse<LicenseDTO>> CreateAsync(NewLicenseDTO newLicense)
        {
            var serviceResponse = new ServiceResponse<LicenseDTO>();

            var license = await _context.Licenses
                .Include(x => x.Designer)
                .SingleOrDefaultAsync(x => x.Content.Trim().Equals(newLicense.Content.Trim()));
            if (license != null)
            {
                serviceResponse.SetError("Nr uprawnień już występuje w bazie", 409);
                return serviceResponse;
            }

            license = _mapper.Map<License>(newLicense);
            license.Designer = _context.Designers.SingleOrDefault(x => x.Id == newLicense.DesignerId);
            var validationResult = await _validator.ValidateAsync(license);
            if (!validationResult.IsValid)
            {
                serviceResponse.ValidationErrorProcess(validationResult);
                return serviceResponse;
            }

            var entity = _context.Licenses.Add(license);
            await _context.SaveChangesAsync();
            serviceResponse.SetData(_mapper.Map<LicenseDTO>(entity.Entity));

            return serviceResponse;
        }

        public override async Task<ServicePageResponse<LicenseDTO>> GetPageAsync(int currentPage = 1, int pageSize = 0)
        {
            _query = _context.Licenses
                .Include(x => x.Designer)
                .Include(x => x.Branches)
                .ThenInclude(x => x.Branch);

            return await base.GetPageAsync(currentPage, pageSize);
        }

        public override async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var license = await _context.Licenses.SingleOrDefaultAsync(x => x.Id == id);
                if(license == null)
                {
                    serviceResponse.SetError($"Błąd: Uprawnienia Id:{id} nie odnalezione.", 401);
                    return serviceResponse;
                }

                var branchRole = await _context.BranchRoles
                    .Include(x => x.PartBranch)
                    .ThenInclude(x => x.ProjectPart)
                    .ThenInclude(x => x.Project)
                    .ThenInclude(x => x.Stage)
                    .Where(x => x.LicenseId == id)
                    .ToListAsync();

                if(branchRole != null)
                {
                    serviceResponse.SetError($"Error: License using in {branchRole[0].PartBranch.ProjectPart.Project}");
                    return serviceResponse;
                }

                _context.Licenses.Remove(license);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public override async Task<ServiceResponse<LicenseDTO>> GetAsync(int id)
        {
            var serviceResponse = new ServiceResponse<LicenseDTO>();

            try
            {
                var license = await _context.Licenses
                    .Include(x => x.Designer)
                    .SingleOrDefaultAsync(x => x.Id == id);
                
                if (license == null)
                {
                    serviceResponse.SetError($"Błąd: Uprawnieania Id:{id} nie odnalezione.", 401);
                    return serviceResponse;
                }

                serviceResponse.SetData(_mapper.Map<LicenseDTO>(license));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<LicenseDTO>> GetBranchesAsync(int id)
        {
            var serviceResponse = new ServiceResponse<LicenseDTO>();

            try
            {
                var license = await _context.Licenses
                    .Include(x => x.Designer)
                    .Include(x => x.Branches)
                    .ThenInclude(x => x.Branch)
                    .SingleOrDefaultAsync(x => x.Id == id);
                serviceResponse.SetData(_mapper.Map<LicenseWithBranchesDTO>(license));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> UpdateAsync(LicenseDTO updateLicense)
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var oldLicense = await _context.Licenses.SingleOrDefaultAsync(x => x.Id == updateLicense.Id);
                if (oldLicense == null)
                {
                    serviceResponse.SetError($"Błąd: Uprawnienia Id:{updateLicense.Id} nie odnalezione.", 401);
                    return serviceResponse;
                }

                oldLicense.Content = updateLicense.Content;
                _context.Licenses.Update(oldLicense);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse> AddBranchAsync(int id, int branchId)
        {
            var result = new ServiceResponse();
            try
            {
                var license = await _context.Licenses
                    .Include(x => x.Branches)
                    .SingleOrDefaultAsync(x => x.Id == id);
                if (license == null)
                {
                    result.SetError("License not found", 401);
                    return result;
                }

                if (license.Branches.SingleOrDefault(x => x.Id == branchId) != null)
                {
                    return result;
                }

                license.Branches.Add(new BranchLicense
                {                              
                    BranchId = branchId,
                    LicenseId = id
                });
                _context.Licenses.Update(license);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, 500);
            }

            return result;
        }

        public async Task<ServiceResponse> RemoveBranchAsync(int id, int branchId)
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var branch = await _context.BranchLicenses.SingleOrDefaultAsync(x => x.LicenseId == id && x.BranchId == branchId);
                if (branch != null)
                {
                    _context.BranchLicenses.Remove(branch);
                    await _context.SaveChangesAsync();
                    serviceResponse.StatusCode = 204;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServicePageResponse<LicenseDTO>> GetLicensesBranchAsync(int branchId)
        {
            var serviceResponse = new ServicePageResponse<LicenseDTO>();
            try
            {
                var licenses = await _context.Licenses
                    .Include(x => x.Branches)
                    .ThenInclude(x => x.Branch)
                    .Include(x => x.Designer)
                    .Where(x => x.Branches.SingleOrDefault(y => y.BranchId == branchId) != null)
                    .ToListAsync();
                serviceResponse.SetData(_mapper.Map<IList<LicenseDTO>>(licenses));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public override async Task<ServicePageResponse<LicenseDTO>> GetPageAsync(bool? active, string searchString, int currentPage = 1, int pageSize = 0)
        {
            var serviceResponse = new ServicePageResponse<LicenseDTO>();

            var pageCollection = _query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            serviceResponse.SetData(_mapper.Map<IList<LicenseDTO>>(await pageCollection.ToListAsync()));

            return await GetPageAsync(currentPage, pageSize);
        }

    }
}
