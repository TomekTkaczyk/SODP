using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Branches;

internal class BranchSearchSpecification : Specification<Branch>
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
			stage.Sign.Value.Contains(searchString) ||
			stage.Title.Value.Contains(searchString)
		))
	{
		AddOrderByExpression(x => x.Order);
		AddOrderByExpression(x => x.Title);
	}
 }
