using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Infrastructure.Specifications.Projects;

public class ProjectByIdSpecification : Specification<Project>
{
	public ProjectByIdSpecification(int id)
		: base(x => x.Id == id)
	{
		AddInclude(x => x.Stage);
	}

	public override Expression<Func<Project, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
