using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IDesignerService : IEntityService<DesignerDTO>
    {
        Task<ServicePageResponse<DesignerDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool? active = false);
        Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
        Task<ServicePageResponse<LicenceDTO>> GetLicencesAsync(int id);
    }
}
