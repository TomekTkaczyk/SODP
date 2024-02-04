using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class LicensesRepository : Repository<License>, ILicensesRepository

{
	public LicensesRepository(SODPDBContext dbContext, ILogger<License> logger) 
		: base(dbContext, logger) { }
}
