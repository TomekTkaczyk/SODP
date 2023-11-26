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
	public static IQueryable<T> WhereIf<T>(
		this IQueryable<T> queryable,
		bool condition,
		Expression<Func<T, bool>> predicate)
	{
		if (condition)
		{
			return queryable.Where(predicate);
		}

		return queryable;
	}

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

		var collection = await query.ToListAsync(cancellationToken);

		return Page<T>.Create(collection, pageNumber, pageSize, totalItems);
	}
}
