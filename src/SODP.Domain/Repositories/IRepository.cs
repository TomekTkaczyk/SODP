using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System.Linq;

namespace SODP.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	TEntity Add(TEntity entity);
	void Delete(TEntity entity);
	void Update(TEntity entity);

	IQueryable<TEntity> ApplySpecyfication(Specification<TEntity> specification);
}