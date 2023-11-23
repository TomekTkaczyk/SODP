using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System.Linq;

namespace SODP.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	IQueryable<TEntity> GetAll(ISpecification<TEntity> specyfication = null);
	TEntity Add(TEntity entity);
	void Delete(TEntity entity);
	void Update(TEntity entity);

	IQueryable<TEntity> ApplySpecyfication(ISpecification<TEntity> specification);
}