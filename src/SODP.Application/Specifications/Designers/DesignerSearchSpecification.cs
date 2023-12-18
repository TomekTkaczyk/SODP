using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;

namespace SODP.Application.Specifications.Designers;

internal sealed class DesignerSearchSpecification : Specification<Designer>
{
	public DesignerSearchSpecification(bool? active = null, string searchString = null)
		: base(designer =>
		(
			!active.HasValue ||
			designer.ActiveStatus.Equals(active)
		)
		&&
		(
			string.IsNullOrWhiteSpace(searchString) ||
			((string)designer.Title).Contains(searchString) ||
			((string)designer.Firstname).Contains(searchString)	||
			((string)designer.Lastname).Contains(searchString)
		))
	{
		AddOrderByExpression(x => x.Title);
	}
}
