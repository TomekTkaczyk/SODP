using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Projects;

internal sealed class ProjectPartByIdSpecification : Specification<ProjectPart>
{
    public ProjectPartByIdSpecification(int id) : base(x => x.Id == id) { }
}
