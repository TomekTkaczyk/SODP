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
			((string)designer.Title).Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
		))
	{
		AddOrderByExpression(x => x.Title);
	}
}
