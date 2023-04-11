using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Designers;

internal class DesignerSearchSpecification : Specification<Designer>
{
	internal DesignerSearchSpecification(bool? active, string searchString)
		: base(designer =>
		(!active.HasValue || designer.ActiveStatus.Equals(active)) &&
		(string.IsNullOrWhiteSpace(searchString)
		|| designer.Firstname.ToLower().Contains(searchString.ToLower()) 
		|| designer.Lastname.ToLower().Contains(searchString.ToLower())))
	{
		AddOrderBy(x => x.Lastname);
		AddOrderBy(x => x.Firstname);
	}
}
