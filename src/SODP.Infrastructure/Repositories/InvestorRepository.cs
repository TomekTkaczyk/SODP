using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class InvestorRepository : PagedRepository<Investor>, IInvestorRepository
{
	public InvestorRepository(SODPDBContext dbContext, ILogger<Investor> logger) : base(dbContext, logger) { }
}
