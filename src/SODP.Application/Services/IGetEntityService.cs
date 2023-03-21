using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IGetEntityService<T> : IAppService where T : BaseDTO
    {
        Task<ServiceResponse<T>> GetAsync(int id);
        Task<ServiceResponse> UpdateAsync(T entity);
        Task<ServiceResponse> DeleteAsync(int id);
		Task<bool> ExistAsync(int id);
	}
}
