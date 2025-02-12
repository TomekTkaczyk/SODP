﻿using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IRoleService : IGetEntityService<RoleDTO>, IActiveStatusService
    
    {
        Task<ServicePageResponse<RoleDTO>> GetPageAsync(bool? active, int currentPage, int pageSize);
    }
}
