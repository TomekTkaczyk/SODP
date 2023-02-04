using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Collections;
using System.Collections.Generic;
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
		Task<ServiceResponse> UpdatePartAsync(int projectPartId, PartDTO part);
		Task<ServiceResponse> DeleteProjectPartAsync(int projectPartId);
		Task<ServiceResponse<ProjectPartDTO>> GetProjectPartAsync(int projectPartId);
		Task<ServiceResponse> AddBranchToPartAsync(int projectPartId, int branchId);
        Task<ServiceResponse> DeletePartBranchAsync(int partBranchId);
		Task<ServiceResponse> AddRoleToPartBranchAsync(int partBranchId, TechnicalRole role, int licenseId);
		Task<ServiceResponse<ProjectPartDTO>> GetProjectPartWithBranchesAsync(int projectPartId);
	}
}
