using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Stages;

public class StageByIdSpecyfication : Specification<Stage>
{
	public StageByIdSpecyfication(int id) 
		: base(stage =>
		stage.Id == id)
	{
	}
}
