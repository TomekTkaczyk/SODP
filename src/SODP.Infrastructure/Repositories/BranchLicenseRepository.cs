using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public sealed class BranchLicenseRepository : Repository<BranchLicense>, IBranchLicenseRepository
{
	public BranchLicenseRepository(SODPDBContext dbContext, ILogger<BranchLicense> logger) 
		: base(dbContext, logger) {	}
}
