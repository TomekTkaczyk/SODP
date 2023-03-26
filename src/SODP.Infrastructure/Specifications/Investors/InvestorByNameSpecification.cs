using SODP.Domain.Entities;

namespace SODP.Infrastructure.Specifications.Investors
{
    internal class InvestorByNameSpecification : Specification<Investor>
    {
        internal InvestorByNameSpecification(bool? active, string name)
            : base(investor =>
            !active.HasValue || investor.ActiveStatus.Equals(active) &&
            (string.IsNullOrWhiteSpace(name) || investor.Name.Contains(name)))
        {
            AddOrderBy(x => x.Name);
        }
    }
}
