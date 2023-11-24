using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Infrastructure.Specifications.Projects;

public class ProjectBySymbolSpecyfication : Specification<Project>
{
	public ProjectBySymbolSpecyfication(string number, string stageSign)
		: base(project =>
		(string.IsNullOrEmpty(number) || project.Number.Equals(number))
		&&
		((string.IsNullOrEmpty(stageSign)) || project.Stage.Sign.Equals(stageSign)))
	{
		AddInclude(i => i.Stage);
	}
}
