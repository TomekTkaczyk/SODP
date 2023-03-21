using SODP.Domain.Entities;
using SODP.Shared.Enums;

namespace SODP.Infrastructure.Managers
{
	public interface IFolderManager
    {
        Task<(bool Success, string Message)> RenameFolderAsync(Project project, ProjectsFolder source);
        Task<(bool Success, string Message)> RenameFolderAsync(Project project, string oldName, ProjectsFolder source);
        Task<(bool Success, string Message)> CreateFolderAsync(Project project);
        Task<(bool Success, string Message)> DeleteFolderAsync(Project project);
        Task<(bool Success, string Message)> ArchiveFolderAsync(Project project);
        Task<(bool Success, string Message)> RestoreFolderAsync(Project project);

    }
}
