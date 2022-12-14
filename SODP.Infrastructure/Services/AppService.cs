using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Services
{
    public class AppService<T> where T : BaseDTO
    {
        public Task<ServicePageResponse<T>> GetAllAsync(int currentPage = 1, int pageSize = 0)
        {
            throw new NotImplementedException();
        }
    }
}
