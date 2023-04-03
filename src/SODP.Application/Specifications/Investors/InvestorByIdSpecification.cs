using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Investors;

internal class InvestorByIdSpecification : Specification<Investor>
{
	internal InvestorByIdSpecification(int id)
		: base(investor => investor.Id == id)
	{
	}
}
