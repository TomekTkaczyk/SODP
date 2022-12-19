using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IDesignerService : IEntityService<DesignerDTO>, IActiveStatusService
    {
        Task<ServicePageResponse<LicenseWithBranchesDTO>> GetLicensesAsync(int id);
        Task<ServiceResponse> AddLicenceAsync(int id, LicenseDTO licence);

	}
}
