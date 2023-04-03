using SODP.Domain.Entities;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IInvestorRepository : IPageRepository<Investor>
{
	//	Task<Page<Investor>> GetPageAsync(
	//		bool? active, 
	//		string searchString, 
	//		int pageNumber,
	//		int pageSize, 
	//		CancellationToken cancellationToken);
}
