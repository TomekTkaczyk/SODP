using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IEntityService<T> : IAppService where T : BaseDTO
    {
        Task<ServiceResponse<T>> CreateAsync(T entity);
        Task<ServicePageResponse<T>> GetAllAsync(int currentPage = 1, int pageSize = 0);
        Task<ServiceResponse<T>> GetAsync(int id);
        Task<ServiceResponse> UpdateAsync(T entity);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
