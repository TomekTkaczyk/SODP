using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Branches;

public class BranchByNameSpecification : Specification<Branch>
{
	public BranchByNameSpecification(bool? active, string searchString)
		: base(branch =>
		(!active.HasValue || branch.ActiveStatus.Equals(active)) &&
		(string.IsNullOrWhiteSpace(searchString) || branch.Name.Contains(searchString) || branch.Sign.Contains(searchString)))
	{
		AddOrderBy(x => x.Name);
	}
}
