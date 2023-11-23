using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Stages;

public class StageByNameSpecification : Specification<Stage>
{
	public StageByNameSpecification(bool? active = null, string searchString = null)
		: base(stage => (
			(!active.HasValue || stage.ActiveStatus.Equals(active)) && (string.IsNullOrEmpty(searchString)
			|| ((string)stage.Sign).Contains(searchString)
			|| stage.Title.Contains(searchString)
			)))
	{
		AddOrderByExpression(x => x.Order);
		AddOrderByExpression(x => x.Sign);
	}
}
