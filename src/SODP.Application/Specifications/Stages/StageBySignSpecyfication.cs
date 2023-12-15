using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Stages;

internal sealed class StageBySignSpecification : Specification<Stage>
{
	public StageBySignSpecification(string sign) : base(stage => stage.Sign.Equals(sign)) { }
}
