using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;

namespace SODP.Application.Specifications.Branches;

internal sealed class BranchSearchSpecification : Specification<Branch>
{
	public BranchSearchSpecification(bool? active = null, string searchString = null)
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
		AddOrderByExpression(x => x.Sign);
	}
 }
