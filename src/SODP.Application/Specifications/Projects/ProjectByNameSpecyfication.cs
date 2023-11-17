using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using SODP.Shared.Enums;
using System.Linq;

namespace SODP.Infrastructure.Specifications.Projects;


public class ProjectByNameSpecyfication : Specification<Project>
{
    public ProjectByNameSpecyfication(ProjectStatus status, string searchString)
        : base(project =>
        (project.Status == status) &&
            (string.IsNullOrWhiteSpace(searchString)
             || project.Name.Contains(searchString)
             || project.Number.Value.Contains(searchString)
             //|| project.Description.Contains(searchString)
             //|| project.Investor.Contains(searchString)
             //|| project.Address.Contains(searchString)
            )
        )
    {
        AddInclude(i => i.Stage);

        AddOrderBy(x => x.Number, false);
        AddOrderBy(x => x.Stage.Sign, false);
    }
}