using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System.Linq;

namespace SODP.Domain.Repositories;

public interface IQueryableRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> Get(ISpecification<TEntity> specification = null);
}
