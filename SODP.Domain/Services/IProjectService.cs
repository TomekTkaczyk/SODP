using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IProjectService : IEntityService<ProjectDTO>
    {
        IProjectService SetArchiveMode();
        IProjectService SetActiveMode();
        Task<ServiceResponse> RestoreAsync(int id);
        Task<ServiceResponse> ArchiveAsync(int id);
        Task<ServicePageResponse<BranchDTO>> GetBranchesAsync(int id);
        Task<ServiceResponse> AddBranchAsync(int projectId, int branchId);
        Task<ServiceResponse> DeleteBranchAsync(int projectId, int branchId);

    }
}
