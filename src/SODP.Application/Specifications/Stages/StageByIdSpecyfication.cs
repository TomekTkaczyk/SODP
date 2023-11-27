using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Stages;

public class StageByIdSpecification : Specification<Stage>
{
	public StageByIdSpecification(int id)
		: base(stage => stage.Id == id) { }
}
