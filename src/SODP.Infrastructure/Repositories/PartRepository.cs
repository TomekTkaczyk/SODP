using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public sealed class PartRepository : Repository<Part>, IPartRepository
{
	public PartRepository(SODPDBContext dbContext, ILogger<Part> logger) 
		: base(dbContext, logger) { }
}
