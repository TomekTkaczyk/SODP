using SODP.Domain.Models;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IProjectsService : IEntityService<ProjectDTO>
    {
        IProjectsService SetArchiveMode();
        IProjectsService SetActiveMode();
        Task<ServiceResponse> RestoreAsync(int id);
        Task<ServiceResponse> ArchiveAsync(int id);
        Task<ServicePageResponse<BranchDTO>> GetBranchesAsync(int id);
        Task<ServiceResponse> AddBranchAsync(int projectId, int branchId);
        Task<ServiceResponse> DeleteBranchAsync(int projectId, int branchId);

    }
}
