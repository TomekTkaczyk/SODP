using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IBranchService : IEntityService<BranchDTO>, IActiveStatusService
    {
        Task<ServicePageResponse<BranchDTO>> GetAllAsync(int currentPage, int pageSize, bool? active = false);
        Task<ServiceResponse<BranchDTO>> GetAsync(string sign);
        Task<ServicePageResponse<LicenseDTO>> GetLicensesAsync(int id);
    }
}
