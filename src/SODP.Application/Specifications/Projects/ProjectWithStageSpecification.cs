using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Projects
{
	public class ProjectWithStageSpecification : Specification<Project>
	{
		public ProjectWithStageSpecification(int stageId)
			: base(project => project.StageId == stageId) { }
	}
}
