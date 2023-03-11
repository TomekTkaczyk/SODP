using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IDesignerService : IEntityService<DesignerDTO>, IActiveStatusService
    {
        Task<ServicePageResponse<DesignerDTO>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize);

        Task<ServicePageResponse<LicenseWithBranchesDTO>> GetLicensesAsync(int id);

        Task<ServiceResponse> AddLicenseAsync(int id, LicenseDTO license);

	}
}
