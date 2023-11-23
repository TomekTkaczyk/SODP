using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

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

	public override Expression<Func<Project, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
