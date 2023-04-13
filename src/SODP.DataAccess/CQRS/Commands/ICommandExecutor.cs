using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Commands;

public interface ICommandExecutor
{
	Task ExecuteAsync<TParameter>(CommandBase<TParameter> command, CancellationToken cancellationToken);
}

public interface ICommandExecutor<TResult>
{
	Task<TResult> ExecuteAsync<TParameter>(CommandBase<TParameter,TResult> command, CancellationToken cancellationToken);
}
