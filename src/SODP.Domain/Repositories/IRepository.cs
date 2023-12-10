using SODP.Domain.Entities;

namespace SODP.Domain.Repositories;

public interface IRepository<TEntity> : IQueryableRepository<TEntity> where TEntity : BaseEntity
{
	TEntity Add(TEntity entity);

	void Update(TEntity entity);

	void Delete(TEntity entity);
}