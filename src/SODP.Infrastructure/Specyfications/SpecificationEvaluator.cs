using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Specyfications
{
	public static class SpecificationEvaluator
	{
		public static IQueryable<TEntity> GetQuery<TEntity>(
			IQueryable<TEntity> inputQueryable, 
			Specification<TEntity> specification) 
			where TEntity : BaseEntity
		{
			IQueryable<TEntity> queryable = inputQueryable;

			if(specification.Criteria is not null)
			{
				queryable = queryable.Where(specification.Criteria);
			}

			queryable = specification.IncludeExpressions.Aggregate(
				queryable, 
				(current, includeExpression) => current.Include(includeExpression));

			if(specification.OrderByExpression is not null)
			{
				queryable = queryable.OrderBy(specification.OrderByExpression);
			}
			else if(specification.OrderByDescendingExpression is not null)
			{
				queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
			}

			return queryable;
		}
	}
}
