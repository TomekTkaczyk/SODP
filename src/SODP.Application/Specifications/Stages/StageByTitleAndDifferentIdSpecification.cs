using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Investors;

internal class StageByTitleAndDifferentIdSpecification : Specification<Stage>
{
	internal StageByTitleAndDifferentIdSpecification(int id, string name)
		: base(stage => stage.Id != id && stage.Title.Equals(name)) { }

	public override Expression<Func<Stage, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
