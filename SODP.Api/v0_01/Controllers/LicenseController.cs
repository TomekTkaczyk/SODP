using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/licenses")]
    public class LicenseController : ApiControllerBase<LicenseDTO>
    {
        public LicenseController(ILicenseService service, ILogger<LicenseController> logger) : base(service, logger) { }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync([FromBody] NewLicenseDTO license)
        {
            return Ok(await (_service as ILicenseService).CreateAsync(license));
        }


        [HttpGet("{id}/branches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetBranchesAsync(int id)
        {
            return Ok(await (_service as ILicenseService).GetBranchesAsync(id));
        }

        [HttpPut("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddBranchAsync(int id, int branchId)
        {
            return Ok(await (_service as ILicenseService).AddBranchAsync(id, branchId));
        }

        [HttpDelete("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveBranchAsync(int id, int branchId)
        {
            return Ok(await (_service as ILicenseService).RemoveBranchAsync(id, branchId));
        }


        [HttpGet("branch/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLicensesBranchAsync(int branchId)
        {
            return Ok(await (_service as ILicenseService).GetLicensesBranchAsync(branchId));
        }
    }
}
