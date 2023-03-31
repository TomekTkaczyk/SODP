using SODP.Domain.Entities;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IStageRepository
{
	Stage Add(Stage stage);
	void Remove(Stage stage);
	void Update(Stage stage);
	Task<Stage> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<Stage> GetBySignAsync(string sign, CancellationToken cancellationToken);
	Task<Page<Stage>> GetPageAsync(bool? active, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken);

}
