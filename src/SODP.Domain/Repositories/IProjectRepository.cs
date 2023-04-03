using SODP.Domain.Entities;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IProjectRepository	: IPageRepository<Project>
{

	//Task<Project> GetBySymbolAsync(string number, string stageSign, CancellationToken cancellationToken);

	Task<Project> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);

	//Task<Page<Project>> GetPageAsync(ProjectStatus status, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
