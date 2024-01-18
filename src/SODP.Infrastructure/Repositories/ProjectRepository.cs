using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public class ProjectRepository : PagedRepository<Project>, IProjectRepository
{
	public ProjectRepository(SODPDBContext dbContext, ILogger<Project> logger) : base(dbContext, logger) { }

	public async Task<Project> GetDetailsAsync(	int id,	CancellationToken cancellationToken)
	{
		var project = await _dbContext.Set<Project>()
			.Include(s => s.Stage)
			.Include(x => x.Parts).ThenInclude(b => b.Branches).ThenInclude(r => r.Roles).ThenInclude(l => l.License).ThenInclude(b => b.Designer)
			.Include(x => x.Parts).ThenInclude(b => b.Branches).ThenInclude(b => b.Branch)
			.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

		return project;
	}
}
