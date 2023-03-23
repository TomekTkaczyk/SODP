using SODP.Domain.Entities;

namespace SODP.Infrastructure.Specyfications
{
	internal class InvestorByIdSpecyfication : Specification<Investor>
	{
		public InvestorByIdSpecyfication(int id) : base(investor => investor.Id == id) { }
	}
}
