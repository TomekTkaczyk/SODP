using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Projects;

internal sealed class PartBranchByIdSpecification : Specification<PartBranch>
{
    public PartBranchByIdSpecification(int id) : base(x => x.Id == id) { }
}
