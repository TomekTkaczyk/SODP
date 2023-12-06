using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;

namespace SODP.Application.Specifications.Stages;

public class StagesSearchSpecification : Specification<Stage>
{
	public StagesSearchSpecification(bool? active = null, string searchString = null)
		: base(stage =>
		(
			!active.HasValue || 
			stage.ActiveStatus.Equals(active)
		) 
		&&
		(
			string.IsNullOrWhiteSpace(searchString) ||
			stage.Sign.Value.Contains(searchString) ||
			stage.Title.Value.Contains(searchString)
		))
	{
		AddOrderByExpression(x => x.Order);
		AddOrderByExpression(x => x.Title);
	}
}
