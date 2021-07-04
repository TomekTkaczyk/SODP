using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.Shared.Response
{
    public class ServicePageResponse<T> : ServiceResponse<PageResponse<T>> where T : BaseDTO
    {
        public ServicePageResponse()
        {
            Data = new PageResponse<T>();
        }

        public void SetData(IList<T> data)
        {
            Data.Collection = data;
            StatusCode = 200;
        }
    }
}