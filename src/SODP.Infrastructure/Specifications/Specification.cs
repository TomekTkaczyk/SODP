using SODP.Domain.Entities;
using System.Linq.Expressions;

namespace SODP.Infrastructure.Specifications;

public abstract class Specification<TEntity> where TEntity : BaseEntity
{
	protected Specification(Expression<Func<TEntity, bool>> criteria)
	{
		Criteria = criteria;
	}

	public bool IsSplitQuery { get; private set; }

	public Expression<Func<TEntity, bool>> Criteria { get; }

	public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

	public List<Tuple<Expression<Func<TEntity, object>>,bool>> OrdersByExpressions { get; } = new();

	protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => IncludeExpressions.Add(includeExpression);

	protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression, bool descending = false) 
		=> OrdersByExpressions.Add( new Tuple<Expression<Func<TEntity, object>>, bool>(orderByExpression, descending));

}
