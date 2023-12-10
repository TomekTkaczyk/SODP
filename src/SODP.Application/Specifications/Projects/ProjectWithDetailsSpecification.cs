using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Infrastructure.Specifications.Projects;

internal class ProjectWithDetailsSpecification : Specification<Project>
{
	internal ProjectWithDetailsSpecification(int id) 
		: base(project => project.Id == id) 
	{
		AddInclude(i => i.Parts);
	}
}
