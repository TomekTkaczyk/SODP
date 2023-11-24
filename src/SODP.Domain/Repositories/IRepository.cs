using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System.Linq;

namespace SODP.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	IQueryable<TEntity> Get(ISpecification<TEntity> specyfication = null);

	TEntity Add(TEntity entity);

	void Update(TEntity entity);

	void Delete(TEntity entity);
}