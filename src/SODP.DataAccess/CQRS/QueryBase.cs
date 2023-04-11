using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS;

public abstract class QueryBase<TResult>
{
	public TResult Result { get; set; }

	public abstract Task<TResult> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken);
}
