using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Branches;

public class BranchByIdSpecification : Specification<Branch>
{
	public BranchByIdSpecification(int id)
		: base(branch => branch.Id == id) { }

	public override Expression<Func<Branch, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
