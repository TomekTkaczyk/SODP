using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IRoleService : IEntityService<RoleDTO>
    {
        Task<ServicePageResponse<RoleDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0);
    }
}
