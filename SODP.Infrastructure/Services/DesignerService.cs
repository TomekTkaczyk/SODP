﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Interfaces;
using SODP.Application.Services;
using SODP.Domain.Helpers;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Services
{
    public class DesignerService : IDesignerService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Designer> _validator;
        private readonly ISODPDBContext _context;

        public DesignerService(IMapper mapper, IValidator<Designer> validator, ISODPDBContext context)
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

                _context.Designers.Remove(designer);
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


        public async Task<bool> DesignerExist(int designerId)
        {
            var result = await _context.Designers.FirstOrDefaultAsync(x => x.Id == designerId);
            return (result != null);
        }
    }
}