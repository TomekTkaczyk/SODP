using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IProjectService : IGetEntityService<ProjectDTO>
    {
        Task<ServiceResponse<ProjectDTO>> CreateAsync(NewProjectDTO project);
        Task<ServicePageResponse<ProjectDTO>> GetPageAsync(ProjectStatus status, int currentPage, int pageSize, string searchString);
        Task<ServiceResponse<ProjectDTO>> GetWithDetailsAsync(int id);
        Task<ServiceResponse> RestoreAsync(int id);
        Task<ServiceResponse> ArchiveAsync(int id);
        Task<ServiceResponse> SetInvestorAsync(int id, string investor);
		Task<ServiceResponse> AddPartAsync(int id, PartDTO part);
		Task<ServiceResponse> DeletePartAsync(int partId);
		Task<ServiceResponse> AddPartBranchAsync(int partId, BranchDTO branch);

		//      Task<ServicePageResponse<ProjectBranchRoleDTO>> GetBranchRolesAsync(int id, int branchId);
		//      Task<ServiceResponse> AddBranchAsync(int id, int branchId);
		//      Task<ServiceResponse> DeleteBranchAsync(int id, int branchId);
		//      Task<ServiceResponse> SetBranchTechnicalRoleAsync(TechnicalRoleDTO technicalRole);
	}
}
