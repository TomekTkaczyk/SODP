using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Investors;

internal class InvestorByNameAndDifferentIdSpecification : Specification<Investor>
{
	internal InvestorByNameAndDifferentIdSpecification(int id, string name)
		: base(investor => investor.Id != id && investor.Name.Equals(name)) { }

	public override Expression<Func<Investor, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
