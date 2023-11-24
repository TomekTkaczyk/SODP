using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Designers;

internal class DesignerSearchSpecification : Specification<Designer>
{
	private readonly bool? _active;
	private readonly string _searchString;

	public DesignerSearchSpecification(bool? active, string searchString)
	{
		_active = active;
		_searchString = searchString;
	}

	public override Expression<Func<Designer, bool>> AsPredicateExpression()
	{
		return designer => (
			(!_active.HasValue || designer.ActiveStatus.Equals(_active)) && (string.IsNullOrEmpty(_searchString)
			|| designer.Firstname.Contains(_searchString)
			|| designer.Lastname.Contains(_searchString)
		));
	}
	//internal DesignerSearchSpecification(bool? active, string searchString)
	//	: base(designer =>
	//	(!active.HasValue || designer.ActiveStatus.Equals(active)) &&
	//	(string.IsNullOrWhiteSpace(searchString)
	//	|| designer.Firstname.ToLower().Contains(searchString.ToLower()) 
	//	|| designer.Lastname.ToLower().Contains(searchString.ToLower())))
	//{
	//	AddOrderBy(x => x.Lastname);
	//	AddOrderBy(x => x.Firstname);
	//}
}
