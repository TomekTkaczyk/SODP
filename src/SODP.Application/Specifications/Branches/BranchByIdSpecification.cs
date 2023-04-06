using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Branches;

public class BranchByIdSpecification : Specification<Branch>
{
	public BranchByIdSpecification(int id)
		: base(branch => branch.Id == id) { }
}
