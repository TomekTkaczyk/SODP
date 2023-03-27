using SODP.Domain.Entities;

namespace SODP.Infrastructure.Specifications.Investors;

    internal class InvestorByNameAndDifferentIdSpecification : Specification<Investor>
    {
        internal InvestorByNameAndDifferentIdSpecification(int id, string name)
            : base(investor => investor.Id != id && investor.Name.Equals(name)) { }
    }
