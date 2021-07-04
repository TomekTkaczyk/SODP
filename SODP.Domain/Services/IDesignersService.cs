using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IDesignersService : IEntityService<DesignerDTO>
    {
        Task<ServicePageResponse<DesignerDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool active = false);
    }
}
