using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IStageRepository : IPageRepository<Stage>
{
	Task<Stage> GetBySignAsync(string sign, CancellationToken cancellationToken);

	//Stage Add(Stage stage);
	//void Delete(Stage stage);
	//void Update(Stage stage);
	//Task<Stage> GetByIdAsync(int id, CancellationToken cancellationToken);
	//Task<Page<Stage>> GetPageAsync(bool? active, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken);

}
