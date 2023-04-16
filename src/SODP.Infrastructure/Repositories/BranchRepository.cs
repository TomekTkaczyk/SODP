using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class BranchRepository : PagedRepository<Branch>, IBranchRepository
{
	public BranchRepository(SODPDBContext dbContext) : base(dbContext) { }

	public async Task<Branch> GetByIdWithDetailsAsync(
		int id, 
		CancellationToken cancellationToken)
	{
		return await _dbContext.Set<Branch>()
			.Include(s => s.Licenses)
			.ThenInclude(s => s.License)
			.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
	}
}
