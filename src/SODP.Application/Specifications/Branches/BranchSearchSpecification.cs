using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Branches;

internal class BranchSearchSpecification : Specification<Branch>
{
	public BranchSearchSpecification(bool? active, string searchString)
		: base(branch =>
		(!active.HasValue || branch.ActiveStatus.Equals(active)) &&
		(string.IsNullOrWhiteSpace(searchString)
		|| branch.Sign.Contains(searchString.ToUpper())
		|| branch.Title.Contains(searchString.ToUpper())))
	{
		AddOrderBy(x => x.Title);
	}
}
