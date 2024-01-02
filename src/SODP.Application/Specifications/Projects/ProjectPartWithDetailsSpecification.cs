using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Projects;

internal sealed class ProjectPartWithDetailsSpecification : Specification<ProjectPart>
{
    public ProjectPartWithDetailsSpecification(int id) : base(x => x.Id == id)
    {                                                                         
        AddInclude(x => x.Branches);
    }
}
