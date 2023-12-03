using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Designers;

internal class DesignerSearchSpecification : Specification<Designer>
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
			designer.Title.Contains(searchString)
		))
	{
		AddOrderByExpression(x => x.Title);
	}
}
