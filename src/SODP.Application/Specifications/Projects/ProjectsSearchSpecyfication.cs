using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using SODP.Shared.Enums;

namespace SODP.Infrastructure.Specifications.Projects;

public class ProjectsSearchSpecyfication : Specification<Project>
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
			((string)project.Number).Contains(searchString) || 
			project.Name.Value.Contains(searchString) || 
			project.Description.Contains(searchString) || 
			project.Title.Value.Contains(searchString)
		))
    {
		AddInclude(x => x.Stage);
		AddOrderByExpression(x => x.Number);
		AddOrderByExpression(x => x.Stage.Sign);
	}
}
