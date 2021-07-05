using System.Collections.Generic;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface IRoleService : IAppService
    {
        Task<IList<string>> GetAllAsync();
    }
}
