using SODP.Domain.Entities;
using SODP.Shared.Enums;

namespace SODP.Infrastructure.Specifications.Projects;

internal class ProjectByNameSpecyfication : Specification<Project>
{
	internal ProjectByNameSpecyfication(ProjectStatus status, string searchString)
		: base(project =>
		project.Status == status && (
		string.IsNullOrWhiteSpace(searchString) 
		|| project.Description.Contains(searchString)
		|| project.Investor.Contains(searchString)
		|| project.Name.Contains(searchString)
		|| project.Number.Contains(searchString)
		|| project.Address.Contains(searchString)))
	{
		AddInclude(i => i.Stage);

		AddOrderBy(x => x.Number, false);
		AddOrderBy(x => x.Stage.Sign, false);
	}
}
