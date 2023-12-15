using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Designers;

internal sealed class DesignerByIdWithLicensesSpecification : Specification<Designer>
{
	public DesignerByIdWithLicensesSpecification(int id)
	: base(investor => investor.Id == id)
	{
		AddInclude(x => x.Licenses);
	}
}
