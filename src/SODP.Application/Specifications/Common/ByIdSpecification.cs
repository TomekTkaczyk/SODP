using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Common;

public sealed class ByIdSpecification<TEntity> : Specification<TEntity> where TEntity : BaseEntity
{
	public ByIdSpecification(int id)
		: base(entity =>
		entity.Id == id)
	{
	}

	public override Expression<Func<TEntity, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
