using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Projects
{
    public class ProjectWithStageSpecification : Specification<Project>
	{
		public ProjectWithStageSpecification(int stageId)
			: base(project => project.StageId == stageId) { }

		public override Expression<Func<Project, bool>> AsPredicateExpression()
		{
			throw new NotImplementedException();
		}
	}
}
