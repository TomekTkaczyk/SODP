using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Infrastructure.Specifications.Stages
{
	public class StageByNameSpecification : Specification<Stage>
	{
		public StageByNameSpecification(bool? active, string searchString)
			: base(stage =>
			(!active.HasValue || stage.ActiveStatus.Equals(active)) &&
			(string.IsNullOrWhiteSpace(searchString) 
			|| stage.Title.Contains(searchString) 
			|| stage.Sign.Contains(searchString)))
		{
			AddOrderBy(x => x.Order);
			AddOrderBy(x => x.Sign);
		}
	}
}
