using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public sealed class StageRepository : PagedRepository<Stage>, IStageRepository
{
	public StageRepository(SODPDBContext dbContext, ILogger<Stage> logger) : base(dbContext, logger) { }
}
