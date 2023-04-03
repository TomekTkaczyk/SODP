using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Infrastructure.Specifications.Projects;

public class ProjectBySymbolSpecyfication : Specification<Project>
{
	public ProjectBySymbolSpecyfication(string number, string stageSign)
		: base(project => 
		project.Number.Equals(number)
		&& project.Stage.Sign.Equals(stageSign))
	{
		AddInclude(i => i.Stage);
	}
}
