using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Branches;

internal sealed class BranchBySignSpecification : Specification<Branch>
{
	public BranchBySignSpecification(string sign) : base(x => x.Sign.Equals(sign)) { }
}
