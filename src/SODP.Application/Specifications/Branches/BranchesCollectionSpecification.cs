﻿using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Branches;

public class BranchesCollectionSpecification : Specification<Branch>
{
	public BranchesCollectionSpecification(bool? active = null, string searchString = null)
		: base(branch =>
		(!active.HasValue || branch.ActiveStatus.Equals(active)) &&
		(string.IsNullOrWhiteSpace(searchString) 
		|| ((string)branch.Sign).Contains(searchString) 
		|| branch.Title.Contains(searchString)))
	{
		AddOrderByExpression(x => x.Order);
		AddOrderByExpression(x => x.Title);
	}
}