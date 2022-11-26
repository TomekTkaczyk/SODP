using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IProjectService : IAppService
    {
        IProjectService SetArchiveMode();
        IProjectService SetActiveMode();
        Task<ServicePageResponse<ProjectDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, string searchString = "");
        Task<ServiceResponse<ProjectDTO>> CreateAsync(NewProjectDTO project);
        Task<ServiceResponse<ProjectDTO>> GetAsync(int id);
        Task<ServiceResponse> UpdateAsync(ProjectDTO project);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse<ProjectDTO>> GetWithBranchesAsync(int id);
        Task<ServiceResponse> RestoreAsync(int id);
        Task<ServiceResponse> ArchiveAsync(int id);
        Task<ServicePageResponse<ProjectBranchRoleDTO>> GetBranchRolesAsync(int id, int branchId);
        Task<ServiceResponse> AddBranchAsync(int id, int branchId);
        Task<ServiceResponse> DeleteBranchAsync(int id, int branchId);
        Task<ServiceResponse> SetBranchTechnicalRoleAsync(int id, int branchId, TechnicalRole role, int licenseId);



    }
}
