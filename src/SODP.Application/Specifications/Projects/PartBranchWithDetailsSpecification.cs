using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Projects;

internal sealed class PartBranchWithDetailsSpecification : Specification<PartBranch>
{
    public PartBranchWithDetailsSpecification(int id) : base(x => x.Id ==  id)
    {              
        AddInclude(x => x.Roles);
    }
}
