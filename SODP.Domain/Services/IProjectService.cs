using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IProjectService : IAppService
    {
        IProjectService SetArchiveMode();
        IProjectService SetActiveMode();
        Task<ServicePageResponse<ProjectDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0);
        Task<ServiceResponse<ProjectDTO>> CreateAsync(NewProjectDTO project);
        Task<ServiceResponse<ProjectDTO>> GetAsync(int id);
        Task<ServiceResponse> UpdateAsync(ProjectDTO project);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse<ProjectDTO>> GetWithBranchesAsync(int id);
        Task<ServiceResponse> RestoreAsync(int id);
        Task<ServiceResponse> ArchiveAsync(int id);
        Task<ServicePageResponse<BranchDTO>> GetBranchesAsync(int id);
        Task<ServiceResponse> AddBranchAsync(int id, int branchId);
        Task<ServiceResponse> DeleteBranchAsync(int id, int branchId);

    }
}
