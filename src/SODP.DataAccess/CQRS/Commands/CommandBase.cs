using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Commands;

public abstract class CommandBase<TParameter,TResult>
{
	public abstract Task<TResult> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken);

}
