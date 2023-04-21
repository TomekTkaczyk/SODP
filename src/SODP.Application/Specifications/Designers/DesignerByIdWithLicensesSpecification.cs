using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Designers;

internal class DesignerByIdWithLicensesSpecification : Specification<Designer>
{
	public DesignerByIdWithLicensesSpecification(int id)
	: base(investor => investor.Id == id)
	{
		AddInclude(x => x.Licenses);
	}
}
