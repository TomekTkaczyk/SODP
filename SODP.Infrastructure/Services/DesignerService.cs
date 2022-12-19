using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
    public class DesignerService : AppService<Designer, DesignerDTO>, IDesignerService
    {
        public DesignerService(IMapper mapper, IValidator<Designer> validator, SODPDBContext context, IActiveStatusService<Designer> activeStatusService) : base(mapper, validator, context, activeStatusService) { }

        public override async Task<ServicePageResponse<DesignerDTO>> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0)
        {
            var query = SetActiveFilter(active)
                .GetQuery()
                .OrderBy(x => x.Lastname)
                .ThenBy(x => x.Firstname);

            var serviceResponse = new ServicePageResponse<DesignerDTO>();
            try
            {
                serviceResponse.Data.TotalCount = await GetQuery().CountAsync();
                if (pageSize == 0)
                {
                    pageSize = serviceResponse.Data.TotalCount;
                }

                var designers = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                serviceResponse.Data.PageNumber = currentPage;
                serviceResponse.Data.PageSize = pageSize;
                serviceResponse.SetData(_mapper.Map<IList<DesignerDTO>>(designers));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<int> GetAsync(DesignerDTO designer)
        {
            var result = await _context.Designers.FirstOrDefaultAsync(x => x.Firstname.Trim().Equals(designer.Firstname.Trim()) && x.Lastname.Trim().Equals(designer.Lastname.Trim()));
            return (result == null ? 0 : result.Id);
        }


        public async Task<ServiceResponse<DesignerDTO>> CreateAsync(DesignerDTO createDesigner)
        {
            var serviceResponse = new ServiceResponse<DesignerDTO>();
            try
            {
                if (await GetAsync(createDesigner) != 0)
                {
                    serviceResponse.SetError($"Projektant {createDesigner} już istnieje.", 409);
                    serviceResponse.ValidationErrors.Add("Designer", "Projektant już istnieje.");
                    return serviceResponse;
                }

                var designer = _mapper.Map<Designer>(createDesigner);
                var validationResult = await _validator.ValidateAsync(designer);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    return serviceResponse;
                }

                designer.Normalize();
                designer.ActiveStatus = true;
                var entity = _context.Designers.Add(designer);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<DesignerDTO>(entity.Entity));

            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;

        }


        public async Task<ServiceResponse> UpdateAsync(DesignerDTO updateDesigner)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var oldDesigner = await _context.Designers.FirstOrDefaultAsync(x => x.Id == updateDesigner.Id);
                if(oldDesigner == null)
                {
                    serviceResponse.SetError($"Błąd: Projektant Id:{updateDesigner.Id} nie odnaleziony.", 401);
                    return serviceResponse;
                }

                var designer = _mapper.Map<Designer>(updateDesigner);
                var validationResult = await _validator.ValidateAsync(designer);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    
                    return serviceResponse;
                }

                designer.Normalize();

                oldDesigner.Title = designer.Title;
                oldDesigner.Firstname = designer.Firstname;
                oldDesigner.Lastname = designer.Lastname;
                _context.Designers.Update(oldDesigner);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<ServicePageResponse<LicenseWithBranchesDTO>> GetLicensesAsync(int id)
        {
            var serviceResponse = new ServicePageResponse<LicenseWithBranchesDTO>();

            try
            {
                var licenses = await _context.Licenses
                    .Include(x => x.Designer)
                    .Include(x => x.Branches)
                    .ThenInclude(y => y.Branch)
                    .Where(z => z.DesignerId == id)
                    .ToListAsync();

                serviceResponse.SetData(_mapper.Map<IList<LicenseWithBranchesDTO>>(licenses));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse> AddLicenceAsync(int id, LicenseDTO newLicense)
        {
            var serviceResponse = new ServiceResponse<LicenseDTO>();

            try
            {
                var license = await _context.Licenses.FirstOrDefaultAsync(x => x.Content.Equals(newLicense.Content));
                if(license != null)
                {
                    serviceResponse.SetError("Numer uprawnień już istnieje", 409);
                    return serviceResponse;
                }

                var licence = _mapper.Map<License>(newLicense);
                licence.Designer = await _context.Designers.FirstOrDefaultAsync(x => x.Id == licence.DesignerId);
                var entity = _context.Licenses.Add(licence);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<LicenseDTO>(entity.Entity));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

	}
}
