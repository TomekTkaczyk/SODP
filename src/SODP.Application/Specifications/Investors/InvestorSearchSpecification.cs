using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Investors;

internal class InvestorSearchSpecification : Specification<Investor>
{
	internal InvestorSearchSpecification(bool? active, string searchString)
		: base(investor =>
		(!active.HasValue || investor.ActiveStatus.Equals(active)) &&
		(string.IsNullOrWhiteSpace(searchString) || investor.Name.Contains(searchString)))
	{
		AddOrderBy(x => x.Name);
	}
}
