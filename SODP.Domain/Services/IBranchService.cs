using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IBranchService : IEntityService<BranchDTO>
    {
        Task<ServicePageResponse<BranchDTO>> GetAllAsync();
        Task<ServiceResponse<BranchDTO>> GetAsync(string sign);

    }
}
