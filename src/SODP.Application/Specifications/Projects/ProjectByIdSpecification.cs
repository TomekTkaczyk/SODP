using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Infrastructure.Specifications.Projects;

internal sealed class ProjectByIdSpecification : Specification<Project>
{
	public ProjectByIdSpecification(int id)
		: base(x => x.Id == id)
	{
		AddInclude(x => x.Stage);
	}
}
