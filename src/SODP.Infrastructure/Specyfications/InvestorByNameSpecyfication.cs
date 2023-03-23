using SODP.Domain.Entities;

namespace SODP.Infrastructure.Specyfications
{
	public class InvestorByNameSpecyfication : Specification<Investor>
	{
        public InvestorByNameSpecyfication(bool? active, string name)
            : base(investor => 
            (!investor.ActiveStatus.HasValue || investor.ActiveStatus.Value.Equals(active)) && 
            (string.IsNullOrWhiteSpace(name) || investor.Name.Contains(name))) 
        {
            AddOrderBy(x => x.Name);
        }
    }
}
