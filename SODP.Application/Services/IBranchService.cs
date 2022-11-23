using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IBranchService : IEntityService<BranchDTO>
    {
        Task<ServicePageResponse<BranchDTO>> GetAllAsync(bool? active = false);
        Task<ServiceResponse<BranchDTO>> GetAsync(string sign);
        Task<ServiceResponse<BranchDTO>> CreateAsync(BranchDTO branch);
        Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
        Task<ServicePageResponse<LicenseDTO>> GetLicensesAsync(int id);
    }
}
