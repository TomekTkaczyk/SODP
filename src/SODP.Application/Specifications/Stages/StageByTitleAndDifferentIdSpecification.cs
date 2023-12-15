using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Investors;

internal sealed class StageByTitleAndDifferentIdSpecification : Specification<Stage>
{
	internal StageByTitleAndDifferentIdSpecification(int id, string name)
		: base(stage => stage.Id != id && stage.Title.Equals(name)) { }
}
