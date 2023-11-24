using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Designers;

public class DesignerLicensesSpecification : Specification<Designer>
{
	public DesignerLicensesSpecification(int id)
		: base(designer =>
		designer.Id == id)
	{
		AddInclude(x => x.Licenses);
	}

	public override Expression<Func<Designer, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
