using SODP.Domain.Entities;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IProjectRepository	: IPageRepository<Project>
{
	Task<Project> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
}
