using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Designers;

public class DesignerByNameSpecification : Specification<Designer>
{
	public DesignerByNameSpecification(bool? active, string firstName, string lastName)
		: base(designer =>
		(!active.HasValue || designer.ActiveStatus.Equals(active)) &&
		designer.Firstname.ToUpper().Equals(firstName.ToUpper()) && designer.Lastname.ToUpper().Equals(lastName.ToUpper()))
	{
	}
}
