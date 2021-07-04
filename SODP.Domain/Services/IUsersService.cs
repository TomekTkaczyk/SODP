using SODP.Domain.Models;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IUsersService : IEntityService<UserDTO>
    {
        Task<ServicePageResponse<UserDTO>> GetAllAsync();
        Task<ServicePageResponse<RoleDTO>> GetRolesAsync(int id);
        Task<ServiceResponse> SetEnable(int id, bool status);
    }
}
