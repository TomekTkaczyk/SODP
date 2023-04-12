using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries;

public class QueryExecutor : IQueryExecutor
{
    private readonly SODPDBContext _context;

    public QueryExecutor(SODPDBContext context)
    {
        _context = context;
    }

    public async Task<TResult> ExecuteAsync<TResult>(QueryBase<TResult> query, CancellationToken cancellationToken)
    {
        return await query.ExecuteAsync(_context, cancellationToken);
    }
}
