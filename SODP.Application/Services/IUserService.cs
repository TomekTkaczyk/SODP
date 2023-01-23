using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IUserService : IEntityService<UserDTO>
    {
        Task<ServicePageResponse<UserDTO>> GetPageAsync(bool? active, int currentPage, int pageSize, string searchString);
        Task<ServicePageResponse<RoleDTO>> GetRolesAsync(int id);
    }
}
