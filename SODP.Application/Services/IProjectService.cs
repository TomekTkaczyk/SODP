using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IProjectService : IEntityService<ProjectDTO>
    {
        IProjectService SetArchiveMode();
        IProjectService SetActiveMode();
        Task<ServiceResponse<ProjectDTO>> CreateAsync(NewProjectDTO project);
        Task<ServicePageResponse<ProjectDTO>> GetPageAsync(int currentPage = 1, int pageSize = 0, string searchString = "");
        Task<ServiceResponse<ProjectDTO>> GetWithBranchesAsync(int id);
        Task<ServiceResponse> RestoreAsync(int id);
        Task<ServiceResponse> ArchiveAsync(int id);
        Task<ServicePageResponse<ProjectBranchRoleDTO>> GetBranchRolesAsync(int id, int branchId);
        Task<ServiceResponse> AddBranchAsync(int id, int branchId);
        Task<ServiceResponse> DeleteBranchAsync(int id, int branchId);
        Task<ServiceResponse> SetBranchTechnicalRoleAsync(TechnicalRoleDTO technicalRole);



    }
}
