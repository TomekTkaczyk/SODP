using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;

namespace SODP.Application.Specifications.Stages;

internal sealed class StagesSearchSpecification : Specification<Stage>
{
	public StagesSearchSpecification(bool? active = null, string searchString = null)
		: base(stage =>
		(
			!active.HasValue || 
			stage.ActiveStatus.Equals(active)
		) 
		&&
		(
			string.IsNullOrWhiteSpace(searchString) ||
			((string)stage.Sign).Contains(searchString) ||
            ((string)stage.Title).Contains(searchString)
		))
	{
		AddOrderByExpression(x => x.Order);
		AddOrderByExpression(x => x.Title);
	}
}
