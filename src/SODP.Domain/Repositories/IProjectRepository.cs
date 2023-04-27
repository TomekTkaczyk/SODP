using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IProjectRepository	: IPageRepository<Project>
{
	Task<Project> GetWithDetailsAsync(int id, CancellationToken cancellationToken);
	Task<ProjectPart> GetPartAsync(int id, CancellationToken cancellationToken);
}
