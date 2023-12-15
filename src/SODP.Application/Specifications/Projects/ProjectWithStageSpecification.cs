using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Projects;

internal sealed class ProjectWithStageSpecification : Specification<Project>
{
	public ProjectWithStageSpecification(int stageId)
		: base(project => project.StageId == stageId) { }
}
