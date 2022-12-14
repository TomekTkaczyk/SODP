using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IUserService : IEntityService<UserDTO>, IActiveStatusService
    {
        Task<ServicePageResponse<RoleDTO>> GetRolesAsync(int id);
    }
}
