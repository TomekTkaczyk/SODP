using SODP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SODP.Domain.Repositories
{
	public interface IStageRepository
	{
		Task<Stage> Create(Stage investor, CancellationToken cancellationToken);
		Task Remove(Stage investor, CancellationToken cancellationToken);
		Task Update(Stage investor, CancellationToken cancellationToken);
		Task<Stage> GetById(int id, CancellationToken cancellationToken);
		Task<ICollection<Stage>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize, CancellationToken cancellationToken);

	}
}
