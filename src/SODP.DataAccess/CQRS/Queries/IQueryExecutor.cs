using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries;

public interface IQueryExecutor
{
    Task<TResult> ExecuteAsync<TResult>(QueryBase<TResult> query, CancellationToken cancellationToken);
}

public interface IQueryExecutor<TResult>
{
	Task<TResult> ExecuteAsync<TResult>(QueryBase<TResult> query, CancellationToken cancellationToken);
}
