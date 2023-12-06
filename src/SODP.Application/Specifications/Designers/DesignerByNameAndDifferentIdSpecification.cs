using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Designers;

public class DesignerByNameAndDifferentIdSpecification : Specification<Designer>
{
	public DesignerByNameAndDifferentIdSpecification(int id, string firstName, string lastName)
	: base(designer =>
	(designer.Id != id) &&
	designer.Firstname.Value.ToUpper().Equals(firstName.ToUpper()) && 
	designer.Lastname.Value.ToUpper().Equals(lastName.ToUpper()))
	{
	}

	public override Expression<Func<Designer, bool>> AsPredicateExpression()
	{
		throw new NotImplementedException();
	}
}
