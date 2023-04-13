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
    public async Task<TResult> ExecuteAsync<TParameter, TResult>(CommandBase<TParameter, TResult> command, CancellationToken cancellationToken)
	{
		return await command.ExecuteAsync(_context, cancellationToken);
	}
}
