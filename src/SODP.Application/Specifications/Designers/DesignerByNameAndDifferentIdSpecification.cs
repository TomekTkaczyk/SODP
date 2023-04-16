using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Designers;

public class DesignerByNameAndDifferentIdSpecification : Specification<Designer>
{
	public DesignerByNameAndDifferentIdSpecification(int id, string firstName, string lastName)
	: base(designer =>
	(designer.Id != id) &&
	designer.Firstname.ToUpper().Equals(firstName.ToUpper()) && 
	designer.Lastname.ToUpper().Equals(lastName.ToUpper()))
	{
	}
}
