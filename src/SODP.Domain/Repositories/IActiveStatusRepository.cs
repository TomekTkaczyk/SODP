using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories
{
	public interface IActiveStatusRepository
	{
		Task SetActiveStatusAsync(int id, bool status, CancellationToken cancellationToken);
    }

	public interface IActiveStatusRepository<in T> : IActiveStatusRepository where T : IActiveStatus { }
}
