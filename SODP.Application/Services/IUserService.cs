using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IUserService : IEntityService<UserDTO>
    {
        Task<ServicePageResponse<UserDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0);
        Task<ServicePageResponse<RoleDTO>> GetRolesAsync(int id);
        Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
    }
}
