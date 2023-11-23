using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Infrastructure.Specifications.Projects;

internal class ProjectWithDetailsSpecification : Specification<Project>
{
	internal ProjectWithDetailsSpecification(int id) 
		: base(project => project.Id == id) 
	{
		AddInclude(i => i.Parts);
	}

	public override Expression<Func<Project, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
