using SODP.Domain.Entities;

namespace SODP.Domain.Repositories
{
	public interface IActiveStatusRepository
	{
    }

	public interface IActiveStatusRepository<in T> : IActiveStatusRepository where T : IActiveStatus
	{ 
		void SetActiveStatus(T entity, bool status);
	}
}
