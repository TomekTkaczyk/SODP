using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Designers;

internal sealed class DesignerLicensesSpecification : Specification<Designer>
{
	public DesignerLicensesSpecification(int id)
		: base(designer =>
		designer.Id == id)
	{
		AddInclude(x => x.Licenses);
	}
}
