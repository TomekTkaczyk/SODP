using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;

namespace SODP.Infrastructure.Repositories
{
	internal abstract class PagedRepository<T> where T : BaseEntity
	{
		private readonly SODPDBContext _dbContext;

		public PagedRepository(SODPDBContext dbContext)
        {
			_dbContext = dbContext;
		}

        public async Task<List<T>> GetPageAsync(IQueryable<T> collection, int currentPage, int pageSize, CancellationToken cancellationToken) 
		{
			return await collection.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
		}

		private IQueryable<T> PageQuery(IQueryable<T> query, int currentPage, int pageSize)
		{
			if (currentPage < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(currentPage), "Error: Required currentPage > 0");
			}

			if (query is IOrderedQueryable<T>)
			{
				query = (query as IOrderedQueryable<T>).ThenBy(x => x.Id);
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
