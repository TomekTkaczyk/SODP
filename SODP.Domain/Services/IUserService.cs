using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IUserService : IEntityService<UserDTO>
    {
        Task<ServicePageResponse<RoleDTO>> GetRolesAsync(int id);
        Task<ServiceResponse> SetEnable(int id, bool status);
    }
}
