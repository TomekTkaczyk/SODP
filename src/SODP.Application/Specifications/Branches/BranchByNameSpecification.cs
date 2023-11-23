using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Branches;

public class BranchByNameSpecification : Specification<Branch>
{
	public BranchByNameSpecification(bool? active, string searchString)
		: base(branch =>
		(!active.HasValue || branch.ActiveStatus.Equals(active)) &&
		(string.IsNullOrWhiteSpace(searchString) 
		|| branch.Sign.ToUpper().Contains(searchString.ToUpper()) 
		|| branch.Title.ToUpper().Contains(searchString.ToUpper())))
	{
		AddOrderByExpression(x => x.Title.ToUpper());
	}

	public override Expression<Func<Branch, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
