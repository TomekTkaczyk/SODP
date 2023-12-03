using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Investors;

internal class InvestorSearchSpecification : Specification<Investor>
{
	public InvestorSearchSpecification(bool? active = null, string searchString = null)
		: base(investor =>
		(
			!active.HasValue ||
			investor.ActiveStatus.Equals(active)
		)
		&&
		(
			string.IsNullOrWhiteSpace(searchString) ||
			investor.Name.Contains(searchString)
		))
	{
		AddOrderByExpression(x => x.Name);
	}
}
