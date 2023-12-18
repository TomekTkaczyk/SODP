using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Designers;

internal sealed class DesignerByNameAndDifferentIdSpecification : Specification<Designer>
{
	public DesignerByNameAndDifferentIdSpecification(int id, string firstName, string lastName)
	: base(designer =>
	(designer.Id != id) &&
	designer.Firstname.Equals(firstName) && 
	designer.Lastname.Equals(lastName))
	{
	}
}
