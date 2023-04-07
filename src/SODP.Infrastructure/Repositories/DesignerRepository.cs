using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class DesignerRepository : PagedRepository<Designer>, IDesignerRepository
{
	public DesignerRepository(SODPDBContext dbContext) : base(dbContext) { }
}
