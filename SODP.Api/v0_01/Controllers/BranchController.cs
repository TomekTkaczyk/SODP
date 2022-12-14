using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("/api/v0_01/branches")]
    public class BranchController : ApiControllerBase<BranchDTO>
    {
        public BranchController(IBranchService service, ILogger<BranchController> logger) :base(service, logger) 
        {
            var aaa = new BranchDTO();
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> GetAllAsync(bool? ActiveOnly)
        //{
        //    return Ok(await _service.GetAllAsync(1,0));
        //}

        [HttpGet("{id}/designers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLicensesAsync(int id)
        {
            return Ok(await (_service as IBranchService).GetLicensesAsync(id));
        }
    }
}
