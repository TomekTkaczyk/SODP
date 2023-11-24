using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
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
}
