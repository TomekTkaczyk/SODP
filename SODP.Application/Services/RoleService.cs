using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(IMapper mapper, RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }


        public async Task<ServicePageResponse<RoleDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0)
        {
            var serviceResponse = new ServicePageResponse<RoleDTO>();

            try
            {
                var response = await _roleManager.Roles.OrderBy(x => x.Name).ToListAsync();
                serviceResponse.Data.Collection = _mapper.Map<IList<RoleDTO>>(response);
                serviceResponse.StatusCode = 200;
                serviceResponse.Data.PageNumber = 1;
                serviceResponse.Data.PageSize = serviceResponse.Data.Collection.Count;
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<RoleDTO>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(RoleDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<RoleDTO>> CreateAsync(RoleDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
