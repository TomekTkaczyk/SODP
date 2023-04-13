using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries.Common;

public class GetByIdQuery<TEntity> : QueryBase<TEntity> where TEntity : BaseEntity
{
	private readonly int _id;

	public GetByIdQuery(int id)
    {
		_id = id;
	}
    public override async Task<TEntity> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
	{
		return await context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == _id, cancellationToken);
	}
}
