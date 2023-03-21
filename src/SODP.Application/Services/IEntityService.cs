using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IEntityService<T> : IGetEntityService<T> where T : BaseDTO
    {
        Task<ServiceResponse<T>> CreateAsync(T entity);
    }
}
