using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IBranchService : IEntityService<BranchDTO>, IActiveStatusService
    {
        Task<ServiceResponse<BranchDTO>> GetAsync(string sign);
        Task<ServicePageResponse<BranchDTO>> GetPageAsync(int currentPage, int pageSize, bool? active);
        Task<ServicePageResponse<LicenseDTO>> GetLicensesAsync(int id);
    }
}
