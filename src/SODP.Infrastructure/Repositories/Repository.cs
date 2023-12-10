using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Domain.Shared.Specifications;

namespace SODP.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	protected readonly SODPDBContext _dbContext;
    protected readonly ILogger<TEntity> _logger;
    protected readonly DbSet<TEntity> _entities;


	public Repository(SODPDBContext dbContext, ILogger<TEntity> logger)
    {
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _entities = _dbContext.Set<TEntity>();
        _logger = logger;
	}

	public IQueryable<TEntity> Get(ISpecification<TEntity> specification = null) =>
		SpecificationEvaluator<TEntity>.GetQuery(_entities, specification);

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
}
