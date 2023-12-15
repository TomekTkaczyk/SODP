using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Investors;

internal sealed class InvestorByNameAndDifferentIdSpecification : Specification<Investor>
{
	internal InvestorByNameAndDifferentIdSpecification(int id, string name)
		: base(investor => investor.Id != id && investor.Name.Equals(name)) { }
}
