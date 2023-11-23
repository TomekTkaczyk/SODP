using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Investors;

internal class InvestorByIdSpecification : Specification<Investor>
{
	internal InvestorByIdSpecification(int id)
		: base(investor => investor.Id == id) { }

	public override Expression<Func<Investor, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
