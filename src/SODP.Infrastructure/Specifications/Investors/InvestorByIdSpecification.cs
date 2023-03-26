using SODP.Domain.Entities;

namespace SODP.Infrastructure.Specifications.Investors
{
	internal class InvestorByIdSpecification : Specification<Investor>
	{
		internal InvestorByIdSpecification(int id)
			: base(investor => investor.Id == id)
		{
		}
	}
}
