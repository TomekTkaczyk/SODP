﻿using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Branches;

public class BranchLicensesSpecification : Specification<Branch>
{
	public BranchLicensesSpecification(int id) 
		: base(branch =>
		branch.Id == id)
	{
		AddInclude(x => x.Licenses);
	}																			   
}