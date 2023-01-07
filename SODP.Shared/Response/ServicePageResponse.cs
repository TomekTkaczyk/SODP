using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.Shared.Response
{
    public class ServicePageResponse<T> : ServiceResponse<Page<T>> where T : BaseDTO
    {
        public ServicePageResponse()
        {
            Data = new Page<T>();
        }

        public void SetData(IList<T> data)
        {
            Data.Collection = data;
            StatusCode = 200;
        }
    }
}