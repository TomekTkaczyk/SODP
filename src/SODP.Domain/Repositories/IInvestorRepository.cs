using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IInvestorRepository
{
	Investor Add(Investor investor);
	void Remove(Investor investor);
	void Update(Investor investor);
	Task<Investor> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<Investor> GetByNameAsync(string name, CancellationToken cancellationToken);
	Task<Page<Investor>> GetPageAsync(bool? active, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
