using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Interfaces;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Domain.Helpers;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<License> _validator;
        private readonly SODPDBContext _context;
        private readonly IDesignerService _designerService;

        public LicenseService(IMapper mapper, IValidator<License> validator, SODPDBContext context, IDesignerService designerService)
        {
            _mapper = mapper;
            _validator = validator;
            _context = context;
            _designerService = designerService;
        }
        public Task<ServiceResponse<LicenseDTO>> CreateAsync(LicenseDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<LicenseDTO>> CreateAsync(NewLicenseDTO newLicense)
        {
            var serviceResponse = new ServiceResponse<LicenseDTO>();

            var license = await _context.Licenses
                .Include(x => x.Designer)
                .FirstOrDefaultAsync(x => x.Content.Trim().Equals(newLicense.Content.Trim()));
            if (license != null)
            {
                serviceResponse.SetError("Nr uprawnień już występuje w bazie", 409);
                return serviceResponse;
            }

            license = _mapper.Map<License>(newLicense);
            license.Designer = _context.Designers.FirstOrDefault(x => x.Id == newLicense.DesignerId);
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


        public async Task<ServicePageResponse<LicenseDTO>> GetPageAsync(int currentPage = 1, int pageSize = 0)
        {
            var serviceResponse = new ServicePageResponse<LicenseDTO>();
            try
            {
                var licenses = await _context.Licenses
                    .Include(x => x.Designer)
                    .ToListAsync();
                serviceResponse.SetData(_mapper.Map<IList<LicenseDTO>>(licenses));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var license = await _context.Licenses.FirstOrDefaultAsync(x => x.Id == id);
                if(license == null)
                {
                    serviceResponse.SetError($"Błąd: Uprawnienia Id:{id} nie odnalezione.", 401);
                    return serviceResponse;
                }

                var projectsWithLicense = await _context.Projects
                    .Include(x => x.Stage)
                    //.Include(x => x.Branches)
                    //.ThenInclude(y => y.DesignerLicense)
                    //.Include(x => x.Branches)
                    //.ThenInclude(y => y.CheckingLicense)
                    .ToListAsync();
                    //.FirstOrDefaultAsync(x => x.DesignerLicenseId == id || x.CheckingLicenseId == id);
                if(projectsWithLicense != null)
                {
                    //serviceResponse.SetError($"Błąd: Uprawnienia użyte w projekcie {projectsWithLicense.Project}");
                    //return serviceResponse;
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

        public async Task<ServiceResponse<LicenseDTO>> GetAsync(int id)
        {
            var serviceResponse = new ServiceResponse<LicenseDTO>();

            try
            {
                var license = await _context.Licenses
                    .Include(x => x.Designer)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
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

        public async Task<ServiceResponse<LicenseWithBranchesDTO>> GetBranchesAsync(int id)
        {
            var serviceResponse = new ServiceResponse<LicenseWithBranchesDTO>();

            try
            {
                var license = await _context.Licenses
                    .Include(x => x.Designer)
                    .Include(x => x.Branches)
                    .ThenInclude(x => x.Branch)
                    .FirstOrDefaultAsync(x => x.Id == id);
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
                var oldLicense = await _context.Licenses.FirstOrDefaultAsync(x => x.Id == updateLicense.Id);
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
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (license == null)
                {
                    result.SetError("License not found", 401);
                    return result;
                }

                if (license.Branches.FirstOrDefault(x => x.Id == branchId) != null)
                {
                    return result;
                }

                license.Branches.Add(new LicenseBranch
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
                var branch = await _context.BranchLicenses.FirstOrDefaultAsync(x => x.LicenseId == id && x.BranchId == branchId);
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
                    .Where(x => x.Branches.FirstOrDefault(y => y.BranchId == branchId) != null)
                    .ToListAsync();
                serviceResponse.SetData(_mapper.Map<IList<LicenseDTO>>(licenses));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

		public Task<bool> ExistAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
