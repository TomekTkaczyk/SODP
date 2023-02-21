using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
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

        public async Task<ServicePageResponse<RoleDTO>> GetPageAsync(bool? active)
        {
            return await GetPageAsync(active, 1, 0);
        }

        public async Task<ServicePageResponse<RoleDTO>> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0)
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

		public Task<bool> ExistAsync(int id)
		{
			throw new NotImplementedException();
		}

        public Task<ServicePageResponse<RoleDTO>> GetPageAsync(int currentPage, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> SetActiveStatusAsync(int id, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
