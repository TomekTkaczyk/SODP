using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Stages;

public class StageBySignSpecification : Specification<Stage>
{
	public StageBySignSpecification(string sign) : base(stage => stage.Sign.Equals(sign)) { }
}
