using Microsoft.AspNetCore.Mvc;
using SODP.Shared.Response;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ILogger<ApiControllerBase> logger;

        public ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            this.logger = logger;
        }

        //public virtual IActionResult GetServiceResponse(ServiceResponse response)
        //{
        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }

        //    return StatusCode(response.StatusCode);
        //}

        //public IActionResult GetResponse<T>(ServiceResponse<T> response)
        //{
        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }

        //    return StatusCode(response.StatusCode);
        //}
    }
}
