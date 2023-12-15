using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Parts;

internal sealed class PartBySignSpecification : Specification<Part>
{
	public PartBySignSpecification(string sign) : base(x => x.Sign.Equals(sign)) { }
}
