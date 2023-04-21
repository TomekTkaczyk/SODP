using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IPageRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	//IQueryable<TEntity> GetPageQuery(IQueryable<TEntity> query, int pageNumber, int pageSize);
	Task<Page<TEntity>> GetPage(
		Specification<TEntity> specification, 
		int pageNumber, 
		int pageSize,
		CancellationToken cancellationToken);
}
