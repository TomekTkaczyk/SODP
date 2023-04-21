using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class InvestorRepository : PagedRepository<Investor>, IInvestorRepository
{
	public InvestorRepository(SODPDBContext dbContext) : base(dbContext) { }
}
