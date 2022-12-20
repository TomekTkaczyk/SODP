using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/designers")]
    public class DesignerController : ApiControllerBase<DesignerDTO>
    {
        public DesignerController(IDesignerService service, ILogger<DesignerController> logger) : base(service, logger) { }

        [HttpGet("{id}/licenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLicensesAsync(int id)
        {
            var response = await (_service as IDesignerService).GetLicensesAsync(id);

            return Ok(response);
        }

        [HttpPost("{id}/licenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddLicense(int id, [FromBody] LicenseDTO license)
        {
            return Ok(await (_service as IDesignerService).AddLicenseAsync(id, license));
        }
    }
}
