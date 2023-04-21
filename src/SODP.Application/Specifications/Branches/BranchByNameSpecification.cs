using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Branches;

public class BranchByNameSpecification : Specification<Branch>
{
	public BranchByNameSpecification(bool? active, string searchString)
		: base(branch =>
		(!active.HasValue || branch.ActiveStatus.Equals(active)) &&
		(string.IsNullOrWhiteSpace(searchString) 
		|| branch.Sign.ToUpper().Contains(searchString.ToUpper()) 
		|| branch.Title.ToUpper().Contains(searchString.ToUpper())))
	{
		AddOrderBy(x => x.Title.ToUpper());
	}
}
