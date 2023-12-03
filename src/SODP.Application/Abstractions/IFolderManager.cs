using SODP.Domain.Entities;
using SODP.Shared.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Abstractions;

public interface IFolderManager
{
	Task<(bool Success, string Message)> CreateProjectFolderAsync(Project project, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> MatchProjectFolderAsync(Project project, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> DeleteProjectFolderAsync(Project project, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> ArchiveProjectFolderAsync(Project project, CancellationToken cancellationToken);
	Task<(bool Success, string Message)> RestoreProjectFolderAsync(Project project, CancellationToken cancellationToken);

}
