using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface ILicenseService : IGetEntityService<LicenseDTO>, IActiveStatusService
    {
        Task<ServicePageResponse<LicenseDTO>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize);

        Task<ServiceResponse<LicenseDTO>> GetBranchesAsync(int id);

        Task<ServiceResponse> AddBranchAsync(int id, int branchId);

        Task<ServiceResponse> RemoveBranchAsync(int id, int branchId);

        Task<ServiceResponse<LicenseDTO>> CreateAsync(NewLicenseDTO newLicense);

        Task<ServicePageResponse<LicenseDTO>> GetLicensesBranchAsync(int branchId);
    }
}
