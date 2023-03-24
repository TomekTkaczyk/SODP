using SODP.Domain.Entities;

namespace SODP.Infrastructure.Specifications.Stages
{
	internal class StageByNameSpecification : Specification<Stage>
	{
		internal StageByNameSpecification(bool? active, string name)
			: base(stage =>
			(!stage.ActiveStatus.HasValue || stage.ActiveStatus.Value.Equals(active)) &&
			(string.IsNullOrWhiteSpace(name) || stage.Name.Contains(name) || stage.Sign.Contains(name)))
		{
			AddOrderBy(x => x.Order);
			AddOrderBy(x => x.Sign);
		}
	}
}
