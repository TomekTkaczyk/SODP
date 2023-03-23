using SODP.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories
{
	public interface IInvestorRepository : IActiveStatusRepository
	{
		Task<Investor> Create(Investor investor, CancellationToken cancellationToken);
		Task Remove(Investor investor, CancellationToken cancellationToken);
		Task Update(Investor investor, CancellationToken cancellationToken);
		Task<Investor> GetById(int id, CancellationToken cancellationToken);
		Task<ICollection<Investor>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize, CancellationToken cancellationToken);
	}
}
