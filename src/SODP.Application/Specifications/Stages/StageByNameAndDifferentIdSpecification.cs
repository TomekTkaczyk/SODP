using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Investors;

internal class StageByNameAndDifferentIdSpecification : Specification<Stage>
{
	internal StageByNameAndDifferentIdSpecification(int id, string name)
		: base(stage => stage.Id != id && stage.Name.Equals(name)) { }
}
