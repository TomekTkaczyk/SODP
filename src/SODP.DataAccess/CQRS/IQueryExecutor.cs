using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS;

public interface IQueryExecutor
{
	Task<TResult> ExecuteAsync<TResult>(QueryBase<TResult> query, CancellationToken cancellationToken);
}
