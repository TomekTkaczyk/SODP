using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.Stages;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Stages;

namespace SODP.Infrastructure.Repositories
{
	public sealed class StageRepository : PagedRepository<Stage>, IStageRepository
	{
		public StageRepository(SODPDBContext dbContext) : base(dbContext) { }

		public async Task<Stage> Create(Stage stage, CancellationToken cancellationToken = default)
		{
			var entity = _dbContext.Set<Stage>().FirstOrDefaultAsync(x => x.Name.Equals(stage.Sign), cancellationToken);
			if (entity != null)
			{
				throw new StageExistException();
			}

			var entry = await _dbContext.Set<Stage>().AddAsync(stage, cancellationToken);

			return entry.Entity;
		}

		public async Task Remove(Stage stage, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Set<Stage>()
				.FirstOrDefaultAsync(x => x.Id == stage.Id, cancellationToken) 
				?? throw new StageNotFoundException();
			_dbContext.Set<Stage>().Remove(entity);
		}

		public async Task Update(Stage stage, CancellationToken cancellationToken)
		{
			var entity = await _dbContext.Set<Stage>()
				.FirstOrDefaultAsync(x => x.Id == stage.Id, cancellationToken) 
				?? throw new StageNotFoundException();
			_dbContext.Set<Stage>().Update(entity);
		}

		public async Task<Stage> GetById(int id, CancellationToken cancellationToken)
		{
			return await _dbContext.Set<Stage>()
				.SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
				?? throw new StageNotFoundException();
		}

		public async Task<ICollection<Stage>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize, CancellationToken cancellationToken)
		{
			var queryable = ApplySpecyfication(new StageByNameSpecification(active, searchString));
			if (pageSize > 0)
			{
				queryable = GetPageQuery(queryable, currentPage, pageSize);
			}

			return await queryable.ToListAsync(cancellationToken);
		}
	}
}
