using SODP.Domain.Models;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IBranchService : IEntityService<BranchDTO>
    {
        Task<ServicePageResponse<BranchDTO>> GetAllAsync();
        Task<ServiceResponse<BranchDTO>> GetAsync(string sign);
    }
}
