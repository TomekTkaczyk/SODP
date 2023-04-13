using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries.Designers;

public class GetDesignerQuery : QueryBase<Designer>
{
	private readonly int _designerId;

	public GetDesignerQuery(int designerId)
	{
		_designerId = designerId;
	}

	public override async Task<Designer> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
	{
		return await context.Set<Designer>()
			.Include(x => x.Licenses)
			.ThenInclude(x => x.Branches)
			.SingleOrDefaultAsync(x => x.Id == _designerId,cancellationToken);
	}
}
