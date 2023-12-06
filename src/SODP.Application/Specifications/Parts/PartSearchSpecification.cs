using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Parts;

internal class PartSearchSpecification : Specification<Part>
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
			part.Sign.Value.Contains(searchString) ||
			part.Title.Value.Contains(searchString)
		))
	{
		AddOrderByExpression(x => x.Order);
		AddOrderByExpression(x => x.Title);
	}
 }
