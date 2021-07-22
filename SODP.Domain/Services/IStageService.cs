using System.Threading.Tasks;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Domain.Services
{
    public interface IStageService : IEntityService<StageDTO>
    {
        Task<ServicePageResponse<StageDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool? active = null);
        Task<ServiceResponse<StageDTO>> GetAsync(string sign);
        Task<ServiceResponse> DeleteAsync(string sign);
        Task<bool> ExistAsync(int id);
        Task<bool> ExistAsync(string sign);
    }
}
