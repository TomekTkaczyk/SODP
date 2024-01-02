using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public class ProjectRepository : PagedRepository<Project>, IProjectRepository
{
	public ProjectRepository(SODPDBContext dbContext, ILogger<Project> logger) : base(dbContext, logger) { }

	public async Task<ProjectPart> GetPartAsync(int id, CancellationToken cancellationToken)
	{
		return await _dbContext.Set<ProjectPart>()
			.Include(x => x.Branches)
			.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public async Task<Project> GetWithDetailsAsync(	int id,	CancellationToken cancellationToken)
	{
		return await _dbContext.Set<Project>()
			.Include(s => s.Stage)
			.Include(x => x.Parts).ThenInclude(b => b.Branches).ThenInclude(r => r.Roles).ThenInclude(l => l.License).ThenInclude(b => b.Designer)
			.Include(x => x.Parts).ThenInclude(b => b.Branches).ThenInclude(b => b.Branch)
			.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	//public async Task<Project> GetBySymbolAsync(
	//	string number,
	//	string stageSign,
	//	CancellationToken cancellationToken)
	//{
	//	return await _dbContext.Set<Project>()
	//		.Include(x => x.Stage)
	//		.Where(x => x.Number.Equals(number) && x.Stage.Sign.Equals(stageSign))
	//		.SingleOrDefaultAsync(cancellationToken);
	//}

	//public async Task<Page<Project>> GetPageAsync(
	//	ProjectStatus status,
	//	string searchString,
	//	int pageNumber,
	//	int pageSize,
	//	CancellationToken cancellationToken)
	//{
	//	var queryable = ApplySpecification(new ProjectByNameSpecification(status, searchString));
		
	//	var totalItems = await queryable.CountAsync(cancellationToken);

	//	if (pageSize > 0)
	//	{
	//		queryable = GetPageQuery(queryable, pageNumber, pageSize);
	//	}

	//	var collection = await queryable.ToListAsync(cancellationToken);

	//	return Page<Project>.Create(
	//		collection,
	//		pageNumber,
	//		pageSize,
	//		totalItems);
	//}
}
