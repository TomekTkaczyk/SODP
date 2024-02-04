using Microsoft.EntityFrameworkCore;
using SODP.Shared.Response;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<Page<T>> AsPageAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var totalItems = await query.CountAsync(cancellationToken);

        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Error: Required pageNumber > 0");
        }

        if (pageSize > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        return new Page<T>(pageNumber, pageSize, totalItems, await query.ToListAsync(cancellationToken));
    }
}
