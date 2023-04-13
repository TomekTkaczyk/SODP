using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries;

public abstract class QueryBase
{
    public abstract Task ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken);
}

public abstract class QueryBase<TResult>
{
	public TResult Result { get; set; }

	public abstract Task<TResult> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken);
}
