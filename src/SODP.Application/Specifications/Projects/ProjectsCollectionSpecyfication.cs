using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using SODP.Shared.Enums;
using System;
using System.Linq.Expressions;

namespace SODP.Infrastructure.Specifications.Projects;

public class ProjectsCollectionSpecyfication : Specification<Project>
{
	public ProjectsCollectionSpecyfication(ProjectStatus status, string searchString)
		: base(project =>
		   (project.Status == status)
		   &&
		   (
				string.IsNullOrWhiteSpace(searchString)
			 || project.Number.Value.Contains(searchString)
		   ))
    {
		AddInclude(x => x.Stage);
		AddOrderByExpression(x => x.Number);
		AddOrderByExpression(x => x.Stage.Sign);
	}
}
