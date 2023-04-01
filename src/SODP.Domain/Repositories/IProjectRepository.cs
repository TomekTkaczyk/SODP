using SODP.Domain.Entities;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IProjectRepository
{
	Task<Page<Project>> GetPageAsync(ProjectStatus status, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
