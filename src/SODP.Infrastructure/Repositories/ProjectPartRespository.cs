using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public class ProjectPartRespository : Repository<ProjectPart>, IProjectPartRepository
{
	public ProjectPartRespository(SODPDBContext dbContext, ILogger<ProjectPart> logger) : base(dbContext, logger) { }

	public async Task<ProjectPart> GetWithDetailsAsync(int id, CancellationToken cancellationToken)
	{
		return await _dbContext.Set<ProjectPart>()
			.Include(x => x.Branches)
			.ThenInclude(y => y.Branch)
			.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
	}
}
