using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Commands;

public abstract class CommandBase<TPartameter,TResult>
{
	public abstract Task<TResult> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken);

}
