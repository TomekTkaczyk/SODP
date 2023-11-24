using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Branches;

internal class BranchSearchSpecification : Specification<Branch>
{
	private readonly bool? _active;
	private readonly string _searchString;

	public BranchSearchSpecification(bool? active, string searchString)
    {
		_active = active;
		_searchString = searchString;
	}

    public override Expression<Func<Branch, bool>> AsPredicateExpression()
	{
		return branch => (
			(!_active.HasValue || branch.ActiveStatus.Equals(_active)) && (string.IsNullOrEmpty(_searchString)
			|| branch.Sign.Contains(_searchString)
			|| branch.Title.Contains(_searchString)
		));
	}


	//public BranchSearchSpecification(bool? active, string searchString)
	//	: base(branch =>
	//	(!active.HasValue || branch.ActiveStatus.Equals(active)) &&
	//	(string.IsNullOrWhiteSpace(searchString)
	//	|| branch.Sign.Contains(searchString.ToUpper())
	//	|| branch.Title.Contains(searchString.ToUpper())))
	//{
	//	AddOrderBy(x => x.Title);
	//}
}
