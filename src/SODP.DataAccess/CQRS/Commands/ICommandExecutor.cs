using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Commands;

public interface ICommandExecutor
{
	Task<TResult> ExecuteAsync<TParameter, TResult>(CommandBase<TParameter,TResult> command, CancellationToken cancellationToken);
}
