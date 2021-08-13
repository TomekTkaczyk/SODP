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
    public class DesignerService : IDesignerService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Designer> _validator;
        private readonly SODPDBContext _context;

        public DesignerService(IMapper mapper, IValidator<Designer> validator, SODPDBContext context)
        {
            _mapper = mapper;
            _validator = validator;
            _context = context;
        }

        public async Task<ServicePageResponse<DesignerDTO>> GetAllAsync()
        {
            return await GetAllAsync(1, 0, null);
        }

        public async Task<ServicePageResponse<DesignerDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool? active = null)
        {
            var serviceResponse = new ServicePageResponse<DesignerDTO>();
            try
            {
                serviceResponse.Data.TotalCount = await _context.Designers.Where(x => active == null || (x.ActiveStatus == active)).CountAsync();
                if(pageSize == 0)                                                
                {
                    pageSize = serviceResponse.Data.TotalCount;
                }
                var designers = await _context.Designers
                    .OrderBy(x => x.Lastname)
                    .ThenBy(y => y.Firstname)
                    .Where(x => active == null || (x.ActiveStatus == active))
                    .Skip((currentPage-1) * pageSize)
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

        public async Task<ServiceResponse<DesignerDTO>> GetAsync(int designerId)
        {
            var serviceResponse = new ServiceResponse<DesignerDTO>();
            try
            {
                var designer = await _context.Designers.FirstOrDefaultAsync(x => x.Id == designerId);
                if (designer == null)
                {
                    serviceResponse.SetError($"Błąd: Projektant Id:{designerId} nie odnaleziony.", 404);
                }
                serviceResponse.SetData(_mapper.Map<DesignerDTO>(designer));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<DesignerDTO>> CreateAsync(DesignerDTO createDesigner)
        {
            var serviceResponse = new ServiceResponse<DesignerDTO>();
            try
            {
                var designer = await _context.Designers.FirstOrDefaultAsync(x => x.Firstname.Trim().Equals(createDesigner.Firstname.Trim()) && x.Lastname.Trim().Equals(createDesigner.Lastname.Trim()));
                if (designer != null)
                {
                    serviceResponse.SetError($"Projektant {createDesigner} już istnieje.", 400);
                    serviceResponse.ValidationErrors.Add("Designer", "Projektant już istnieje.");
                    return serviceResponse;
                }

                designer = _mapper.Map<Designer>(createDesigner);
                var validationResult = await _validator.ValidateAsync(designer);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    return serviceResponse;
                }

                designer.Normalize();
                designer.ActiveStatus = true;
                var entity = await _context.AddAsync(designer);
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

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var designer = await _context.Designers.FirstOrDefaultAsync(x => x.Id == id);
                if(designer == null)
                {
                    serviceResponse.SetError($"Błąd: Projektant Id:{id} nie odnaleziony.", 401);
                    return serviceResponse;
                }

                _context.Entry(designer).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> SetActiveStatusAsync(int id, bool status)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var designer = await _context.Designers.FirstOrDefaultAsync(x => x.Id == id);

                if (designer == null)
                {
                    serviceResponse.SetError($"Projektant Id:{id} nie odnaleziony.", 404);

                    return serviceResponse;
                }

                designer.ActiveStatus = status;
                _context.Designers.Update(designer);
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
                    .ThenInclude(x => x.Branch)
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
                    serviceResponse.SetError("Licence exist", 409);
                    return serviceResponse;
                }

                var licence = _mapper.Map<License>(newLicense);
                licence.Designer = await _context.Designers.FirstOrDefaultAsync(x => x.Id == licence.DesignerId);
                var entity = await _context.AddAsync(licence);
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
