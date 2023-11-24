using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SODP.Domain.Shared.Specifications;

public abstract class Specification<T> : ISpecification<T>
{

    private readonly List<Expression<Func<T, object>>> _includeCollection = new();
    private readonly List<Tuple<Expression<Func<T, object>>, bool>> _ordersByExpressionCollection = new();

    public Expression<Func<T, bool>> Criteria { get; }
    public List<Tuple<Expression<Func<T, object>>, bool>> OrderByExpressions
    {
        get
        {
            return _ordersByExpressionCollection;
        }
    }
    public List<Expression<Func<T, object>>> Includes
    {
        get
        {
            return _includeCollection;
        }
    }

    protected Specification() { }

    protected Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
        => Includes.Add(includeExpression);

    protected void AddOrderByExpression(Expression<Func<T, object>> orderExpression, bool descending = false)
        => OrderByExpressions.Add(new Tuple<Expression<Func<T, object>>, bool>(orderExpression, descending));

    public virtual Expression<Func<T, bool>> AsPredicateExpression()
        => Criteria;

    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.Criteria;
    }

    public static OrSpecification<T> operator |(Specification<T> left, Specification<T> right)
    {
        return left.Or(right);
    }

    public static AndSpecification<T> operator &(Specification<T> left, Specification<T> right)
    {
        return left.And(right);
    }

}
