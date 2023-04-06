using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class BranchRepository : PagedRepository<Branch>, IBranchRepository
{
	public BranchRepository(SODPDBContext dbContext) : base(dbContext) { }
}
