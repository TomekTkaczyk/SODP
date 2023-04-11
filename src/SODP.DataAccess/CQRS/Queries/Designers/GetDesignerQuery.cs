using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries.Designers;

public class GetDesignerQuery : QueryBase<Designer>
{
	private readonly int _id;

	public GetDesignerQuery(int id)
    {
		_id = id;
	}
    public override async Task<Designer> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
	{
		return await context.Set<Designer>().SingleOrDefaultAsync(x => x.Id == _id, cancellationToken);
	}
}
