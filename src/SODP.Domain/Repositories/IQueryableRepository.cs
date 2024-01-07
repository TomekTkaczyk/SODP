using SODP.Domain.Shared.Specifications;
using System.Linq;

namespace SODP.Domain.Repositories;

public interface IQueryableRepository<TEntity>
{
    IQueryable<TEntity> Get(ISpecification<TEntity> specification = null);
}
