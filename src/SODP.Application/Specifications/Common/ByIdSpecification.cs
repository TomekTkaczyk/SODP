using SODP.Domain.Entities;
using SODP.Domain.Specifications;

namespace SODP.Application.Specifications.Common;

public sealed class ByIdSpecification<TEntity> : Specification<TEntity> where TEntity : BaseEntity
{
	public ByIdSpecification(int id)
		: base(entity =>
		entity.Id == id)
	{
	}
}
