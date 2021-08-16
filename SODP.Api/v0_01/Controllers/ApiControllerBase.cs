using Microsoft.AspNetCore.Mvc;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {

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
