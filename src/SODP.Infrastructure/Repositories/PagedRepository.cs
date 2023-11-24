using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Domain.Shared.Specifications;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Repositories;

public abstract class PagedRepository<TEntity> : Repository<TEntity>, IPageRepository<TEntity> where TEntity : BaseEntity
{
	public PagedRepository(SODPDBContext dbContext, ILogger<TEntity> logger) : base(dbContext, logger) { }

	public async Task<Page<TEntity>> GetPageAsync(ISpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
	{

		// var queryable = SpecificationEvaluator<TEntity>.GetQuery(_entities, specification);
		var queryable = Get(specification);

		var totalItems = await queryable.CountAsync(cancellationToken);
		var collection = await GetPageQuery(
			queryable,
			pageNumber,
			pageSize).ToListAsync(cancellationToken);

		return Page<TEntity>.Create(collection, pageNumber, pageSize, totalItems);
	}

	private static IQueryable<TEntity> GetPageQuery(IQueryable<TEntity> query, int pageNumber, int pageSize)
	{
		if (pageNumber < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(pageNumber), "Error: Required pageNumber > 0");
		}

		if (query is IOrderedQueryable<TEntity>)
		{
			query = (query as IOrderedQueryable<TEntity>).ThenBy(x => x.Id);
		}
		else
		{
			query = query.OrderBy(x => x.Id);
		}

		if (pageSize > 0)
		{
			query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
		}

		return query.AsNoTracking();
	}
}
