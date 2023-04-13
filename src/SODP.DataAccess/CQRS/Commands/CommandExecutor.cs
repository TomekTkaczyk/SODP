using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Commands;

public class CommandExecutor : ICommandExecutor
{
	private readonly SODPDBContext _context;

	public CommandExecutor(SODPDBContext context)
    {
		_context = context;
	}
    public async Task ExecuteAsync<TParameter>(CommandBase<TParameter> command, CancellationToken cancellationToken)
	{
		await command.ExecuteAsync(_context, cancellationToken);
	}
}

public class CommandExecutor<TResult> : ICommandExecutor<TResult>
{
	private readonly SODPDBContext _context;

	public CommandExecutor(SODPDBContext context)
	{
		_context = context;
	}

	public async Task<TResult> ExecuteAsync<TParameter>(CommandBase<TParameter, TResult> command, CancellationToken cancellationToken)
	{
		return await command.ExecuteAsync(_context, cancellationToken);
	}
}
