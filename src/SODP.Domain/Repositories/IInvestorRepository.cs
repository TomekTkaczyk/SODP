using SODP.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories
{
	public interface IInvestorRepository : IActiveStatusRepository
	{
		Investor Add(Investor investor);
		void Remove(Investor investor);
		void Update(Investor investor);
		Task<Investor> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<Investor> GetByNameAsync(string name, CancellationToken cancellationToken);
		Task<ICollection<Investor>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize, CancellationToken cancellationToken);
	}
}
