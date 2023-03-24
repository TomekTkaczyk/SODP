using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;

namespace SODP.Infrastructure.Repositories
{
	public abstract class PagedRepository<TEntity> : Repository<TEntity> where TEntity : BaseEntity
	{
		public PagedRepository(SODPDBContext dbContext)	: base(dbContext) { }

        public async Task<List<TEntity>> GetPageAsync(IQueryable<TEntity> collection, int currentPage, int pageSize, CancellationToken cancellationToken) 
		{
			return await collection.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
		}

		protected IQueryable<TEntity> GetPageQuery(IQueryable<TEntity> query, int currentPage, int pageSize)
		{
			if (currentPage < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(currentPage), "Error: Required currentPage > 0");
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
				query = query.Skip((currentPage - 1) * pageSize).Take(pageSize);
			}

			return query.AsNoTracking();
		}
	}
}
