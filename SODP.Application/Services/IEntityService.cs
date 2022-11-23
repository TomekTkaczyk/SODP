using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IEntityService<T> : IAppService where T : BaseDTO
    {
        Task<ServicePageResponse<T>> GetAllAsync();
        Task<ServiceResponse<T>> GetAsync(int id);

        //Task<ServiceResponse<T>> CreateAsync(T entity);
        
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> UpdateAsync(T entity);
    }
}
