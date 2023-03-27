using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;

namespace SODP.Infrastructure.Repositories
{
	public abstract class PagedRepository<TEntity> : Repository<TEntity> where TEntity : BaseEntity
	{
		public PagedRepository(SODPDBContext dbContext)	: base(dbContext) { }

        public async Task<List<TEntity>> GetPageAsync(IQueryable<TEntity> collection, int pageNumber, int pageSize, CancellationToken cancellationToken) 
		{
			return await collection.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
		}

		protected IQueryable<TEntity> GetPageQuery(IQueryable<TEntity> query, int pageNumber, int pageSize)
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
}
