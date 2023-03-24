using SODP.Domain.Entities;

namespace SODP.Infrastructure.Specifications.Investors
{
    internal class InvestorByNameSpecyfication : Specification<Investor>
    {
        internal InvestorByNameSpecyfication(bool? active, string name)
            : base(investor =>
            (!investor.ActiveStatus.HasValue || investor.ActiveStatus.Value.Equals(active)) &&
            (string.IsNullOrWhiteSpace(name) || investor.Name.Contains(name)))
        {
            AddOrderBy(x => x.Name);
        }
    }
}
