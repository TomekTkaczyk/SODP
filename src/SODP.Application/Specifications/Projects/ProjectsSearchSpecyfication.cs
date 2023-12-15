using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using SODP.Shared.Enums;
using System;

namespace SODP.Infrastructure.Specifications.Projects;

internal sealed class ProjectsSearchSpecyfication : Specification<Project>
{
	public ProjectsSearchSpecyfication(ProjectStatus? status = null, string searchString = null)
		: base(project =>
		(
			(status == null) ||
			project.Status.Equals(status)
		)
		&&
		(
			string.IsNullOrWhiteSpace(searchString) ||
			((string)project.Number).Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
			((string)project.Name).Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
			((string)project.Title).Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
            project.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
		))
    {
		AddInclude(x => x.Stage);
		AddOrderByExpression(x => x.Number);
		AddOrderByExpression(x => x.Stage.Sign);
	}
}
