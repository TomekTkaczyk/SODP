using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IBranchService : IEntityService<BranchDTO>, IActiveStatusService
	{
        Task<ServicePageResponse<BranchDTO>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize);
        Task<ServiceResponse<BranchDTO>> GetAsync(string sign);
        Task<ServicePageResponse<LicenseDTO>> GetLicensesAsync(int id);
    }
}
