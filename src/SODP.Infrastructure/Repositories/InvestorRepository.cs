using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class InvestorRepository : PagedRepository<Investor>, IInvestorRepository
{
	public InvestorRepository(SODPDBContext dbContext) : base(dbContext) { }

	//public async Task<Page<Investor>> GetPageAsync(bool? active, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
	//{
	//	var queryable = ApplySpecyfication(new InvestorByNameSpecification(active, searchString));
	//	var totalItems = await queryable.CountAsync(cancellationToken);

	//	if (pageSize > 0)
	//	{
	//		//queryable = queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
	//		queryable = GetPageQuery(queryable, pageNumber, pageSize);
	//	}

	//	var collection = await queryable.ToListAsync(cancellationToken);

	//	return Page<Investor>.Create(
	//		collection,
	//		pageNumber,
	//		pageSize,
	//		totalItems);
	//}
}
