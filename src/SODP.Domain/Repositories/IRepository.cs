using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	IQueryable<TEntity> GetAll();
	TEntity Add(TEntity entity);
	void Delete(TEntity entity);
	void Update(TEntity entity);

	IQueryable<TEntity> ApplySpecyfication(Specification<TEntity> specification);
}