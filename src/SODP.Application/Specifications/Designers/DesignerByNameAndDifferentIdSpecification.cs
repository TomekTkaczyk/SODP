using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Designers;

internal sealed class DesignerByNameAndDifferentIdSpecification : Specification<Designer>
{
	public DesignerByNameAndDifferentIdSpecification(int id, string firstName, string lastName)
	: base(designer =>
	(designer.Id != id) &&
	designer.Firstname.Value.ToUpper().Equals(firstName.ToUpper()) && 
	designer.Lastname.Value.ToUpper().Equals(lastName.ToUpper()))
	{
	}
}
