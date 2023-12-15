using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Infrastructure.Specifications.Projects;

internal sealed class ProjectBySymbolSpecification : Specification<Project>
{
	public ProjectBySymbolSpecification(string number, string stageSign)
		: base(project =>
		(string.IsNullOrEmpty(number) || project.Number.Equals(number))
		&&
		((string.IsNullOrEmpty(stageSign)) || project.Stage.Sign.Equals(stageSign)))
	{
		AddInclude(i => i.Stage);
	}
}
