using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Branches;

internal sealed class BranchLicensesSpecification : Specification<Branch>
{
	public BranchLicensesSpecification(int id) 
		: base(branch =>
		branch.Id == id)
	{
		AddInclude(x => x.Licenses);
	}

	public override Expression<Func<Branch, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
