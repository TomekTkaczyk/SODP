using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;

namespace SODP.Application.Specifications.Parts;

internal sealed class PartSearchSpecification : Specification<Part>
{
	public PartSearchSpecification(bool? active = null, string searchString = null)
		: base(part =>
		(
			!active.HasValue ||
			part.ActiveStatus.Equals(active)
		)
		&&
		(
			string.IsNullOrWhiteSpace(searchString) ||
			((string)part.Sign).Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
			((string)part.Title).Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
		))
	{
		AddOrderByExpression(x => x.Order);
		AddOrderByExpression(x => x.Title);
	}
 }
