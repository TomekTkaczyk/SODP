using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Designers;

public class DesignerLicensesSpecification : Specification<Designer>
{
	public DesignerLicensesSpecification(int id)
		: base(designer =>
		designer.Id == id)
	{
		AddInclude(x => x.Licenses);
	}
}
