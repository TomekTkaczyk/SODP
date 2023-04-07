using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public class PartRepository : PagedRepository<Part>, IPartRepository
{
	public PartRepository(SODPDBContext dbContext) : base(dbContext) { }
}
