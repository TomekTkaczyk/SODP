using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Infrastructure.Repositories;

public abstract class Repository<TEntity> where TEntity : BaseEntity
{
	protected readonly SODPDBContext _dbContext;

	public Repository(SODPDBContext dbContext)
    {
		_dbContext = dbContext;
	}

	public TEntity Add(TEntity entity)
	{
		return _dbContext.Set<TEntity>().Add(entity).Entity;
	}

	public void Update(TEntity entity)
	{
		_dbContext.Set<TEntity>().Update(entity);
		_dbContext.Entry(entity).State = EntityState.Modified;
	}

	public void Delete(TEntity entity)
	{
		_dbContext.Entry(entity).State = EntityState.Deleted;
	}

	public IQueryable<TEntity> ApplySpecyfication(Specification<TEntity> specification)
	{
		return SpecificationEvaluator.GetQuery(_dbContext.Set<TEntity>(), specification);
	}
}
