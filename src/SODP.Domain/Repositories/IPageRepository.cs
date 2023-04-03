using SODP.Domain.Entities;
using System.Linq;

namespace SODP.Domain.Repositories;

public interface IPageRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	IQueryable<TEntity> GetPageQuery(IQueryable<TEntity> query, int pageNumber, int pageSize);
}
