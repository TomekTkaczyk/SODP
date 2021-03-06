using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IBranchService : IEntityService<BranchDTO>
    {
        Task<ServicePageResponse<BranchDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool? active = null);
        Task<ServiceResponse<BranchDTO>> GetAsync(string sign);
        Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
        Task<ServicePageResponse<LicenseDTO>> GetLicensesAsync(int id);
        ServiceResponse Test();
    }
}
