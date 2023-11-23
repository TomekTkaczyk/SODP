using SODP.Domain.Entities;
using SODP.Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace SODP.Application.Specifications.Investors;

internal class InvestorSearchSpecification : Specification<Investor>
{
	private readonly bool? _active;
	private readonly string _searchString;

	public InvestorSearchSpecification(bool? active, string searchString)
    {
		_active = active;
		_searchString = searchString;
	}

    public override Expression<Func<Investor, bool>> AsPredicateExpression()
	{
		return investor => (
			(!_active.HasValue || investor.ActiveStatus.Equals(_active)) && (string.IsNullOrEmpty(_searchString)
			|| investor.Name.Contains(_searchString)
		));
	}

	//internal InvestorSearchSpecification(bool? active, string searchString)
	//	: base(investor =>
	//	(!active.HasValue || investor.ActiveStatus.Equals(active)) &&
	//	(string.IsNullOrWhiteSpace(searchString) || investor.Name.Contains(searchString)))
	//{
	//	AddOrderBy(x => x.Name);
	//}
}
