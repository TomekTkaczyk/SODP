using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Stages;

public class StageBySignSpecyfication : Specification<Stage>
{
	public StageBySignSpecyfication(string sign)
		: base(stage =>	stage.Sign.Equals(sign)) { }
}
