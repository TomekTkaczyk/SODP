using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;

namespace SODP.Application.Specifications.Investors;

internal sealed class InvestorSearchSpecification : Specification<Investor>
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
			((string)investor.Name).Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
		))
	{
		AddOrderByExpression(x => x.Name);
	}
}
