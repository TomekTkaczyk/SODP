using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Domain.Specifications;

namespace SODP.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	protected readonly SODPDBContext _dbContext;
	protected readonly DbSet<TEntity> _entities;


	public Repository(SODPDBContext dbContext)
    {
		_dbContext = dbContext;
	}

	public IQueryable<TEntity> GetAll()
	{
		return _entities;
	}

	public TEntity Add(TEntity entity)
	{
		_entities.Add(entity);
		return entity;
	}

	public void Update(TEntity entity)
	{
		_entities.Update(entity);
		_dbContext.Entry(entity).State = EntityState.Modified;
	}

	public void Delete(TEntity entity)
	{
		_dbContext.Entry(entity).State = EntityState.Deleted;
	}

	public IQueryable<TEntity> ApplySpecyfication(Specification<TEntity> specification)
	{
		return SpecificationEvaluator.GetQuery(_entities, specification);
	}

}
