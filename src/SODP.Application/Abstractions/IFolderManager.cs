using SODP.Domain.Entities;
using SODP.Shared.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Abstractions;

public interface IFolderManager
{
	Task<(bool Success, string Message)> RenameFolderAsync(Project project, ProjectsFolder source, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> RenameFolderAsync(Project project, string oldName, ProjectsFolder source, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> CreateFolderAsync(Project project, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> DeleteFolderAsync(Project project, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> ArchiveFolderAsync(Project project, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> RestoreFolderAsync(Project project, CancellationToken cancellationToken);

}
