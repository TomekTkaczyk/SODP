using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Parts;

internal class PartSearchSpecification : Specification<Part>
{
	private readonly bool? _active;
	private readonly string _searchString;

	public PartSearchSpecification(bool? active, string searchString)
    {
		_active = active;
		_searchString = searchString;
	}

    public override Expression<Func<Part, bool>> AsPredicateExpression()
	{
		return part => (
			(!_active.HasValue || part.ActiveStatus.Equals(_active)) && (string.IsNullOrEmpty(_searchString)
			|| part.Sign.Contains(_searchString)
			|| part.Title.Contains(_searchString)
		));
	}

	//public PartSearchSpecification(bool? active, string searchString)
	//	: base(branch =>
	//	(!active.HasValue || branch.ActiveStatus.Equals(active)) &&
	//	(string.IsNullOrWhiteSpace(searchString)
	//	|| branch.Sign.Contains(searchString.ToUpper())
	//	|| branch.Title.Contains(searchString.ToUpper())))
	//{
	//	AddOrderBy(x => x.Title);
	//}
}
