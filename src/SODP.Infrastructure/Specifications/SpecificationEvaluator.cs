using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace SODP.Infrastructure.Specifications;

public static class SpecificationEvaluator
{
	public static IQueryable<TEntity> GetQuery<TEntity>(
		IQueryable<TEntity> inputQueryable,
		Specification<TEntity> specification)
		where TEntity : BaseEntity
	{
		IQueryable<TEntity> queryable = inputQueryable;

		if (specification.Criteria is not null)
		{
			queryable = queryable.Where(specification.Criteria);
		}

		queryable = specification.IncludeExpressions.Aggregate(
			queryable,
			(current, includeExpression) => current.Include(includeExpression));

		if(specification.OrdersByExpressions.Count > 0)
		{
			var orderQueryable = specification.OrdersByExpressions.First().Item2
				? queryable.OrderByDescending(specification.OrdersByExpressions.First().Item1)
				: queryable.OrderBy(specification.OrdersByExpressions.First().Item1);

			foreach(var expression in specification.OrdersByExpressions.Skip(1))
			{
				orderQueryable = specification.OrdersByExpressions.First().Item2
					? orderQueryable.ThenByDescending(specification.OrdersByExpressions.First().Item1)
					: orderQueryable.ThenBy(specification.OrdersByExpressions.First().Item1);
			}

			return orderQueryable;
		}

		return queryable;
	}
}
