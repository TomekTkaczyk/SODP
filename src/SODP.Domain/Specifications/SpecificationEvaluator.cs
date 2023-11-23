using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SODP.Domain.Specifications;

public static class SpecificationEvaluator<T> where T : class
{
	public static IQueryable<T> GetQuery(IQueryable<T> sourceQuery, ISpecification<T> specification)
	{
		var query = sourceQuery;

		if(specification is null)
		{
			return query;
		}

		if (specification.Criteria is not null) 
		{
			query = query.Where(specification.Criteria);
		}

		query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

		if (specification.OrderByExpressions.Count > 0)
		{
			var orderQueryable = specification.OrderByExpressions.First().Item2
				? query.OrderByDescending(specification.OrderByExpressions.First().Item1)
				: query.OrderBy(specification.OrderByExpressions.First().Item1);

			foreach (var expression in specification.OrderByExpressions.Skip(1))
			{
				orderQueryable = expression.Item2
					? orderQueryable.ThenByDescending(expression.Item1)
					: orderQueryable.ThenBy(expression.Item1);
			}

			query = orderQueryable;
		}

		return query;
	}
}
