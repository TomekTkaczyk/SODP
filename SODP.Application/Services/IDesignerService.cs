using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IDesignerService : IEntityService<DesignerDTO>
    {
        Task<ServicePageResponse<DesignerDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool? active = false);
        Task<int> GetAsync(DesignerDTO designer);
        Task<ServiceResponse<DesignerDTO>> CreateAsync(DesignerDTO designer);
        Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
        Task<ServicePageResponse<LicenseWithBranchesDTO>> GetLicensesAsync(int id);
        Task<ServiceResponse> AddLicenceAsync(int id, LicenseDTO licence);
        Task<bool> DesignerExist(int designerId);
    }
}
