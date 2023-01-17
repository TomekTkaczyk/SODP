using SODP.Model;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
	public interface IActiveStatusService
	{
		Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
		IAppService GetActiveStatus(bool status);
    }

	public interface IActiveStatusService<in T> : IActiveStatusService where T : BaseEntity { }
}
