using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Infrastructure.Specifications;

namespace SODP.Infrastructure.Repositories
{
	public abstract class Repository<TEntity> where TEntity : BaseEntity
	{
		protected readonly SODPDBContext _dbContext;

		public Repository(SODPDBContext dbContext)
        {
			_dbContext = dbContext;
		}


		public IQueryable<TEntity> ApplySpecyfication(Specification<TEntity> specification)
		{
			return SpecificationEvaluator.GetQuery(_dbContext.Set<TEntity>(), specification);
		}
	}
}
