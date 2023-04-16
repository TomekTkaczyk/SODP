using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class LicensesRepository : Repository<License>, ILicensesRepository

{
	public LicensesRepository(SODPDBContext dbContext) : base(dbContext) { }

	public IEnumerable<License> GetAll(int designerId)
		=> _entities
		.Include(x => x.Designer)
		.ToList();
}
