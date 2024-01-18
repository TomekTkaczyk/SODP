using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IProjectRepository	: IPageRepository<Project>
{
	Task<Project> GetDetailsAsync(int id, CancellationToken cancellationToken);
}
