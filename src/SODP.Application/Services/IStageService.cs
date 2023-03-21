using System.Threading.Tasks;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.Services
{
    public interface IStageService : IEntityService<StageDTO>, IActiveStatusService
    {
        Task<ServicePageResponse<StageDTO>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize);
        Task<ServiceResponse<StageDTO>> GetAsync(string sign);
        Task<ServiceResponse> DeleteAsync(string sign);
        Task<bool> ExistAsync(string sign);
    }
}
