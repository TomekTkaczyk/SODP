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
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _service;
        private readonly ILogger<LicenseController> _logger;

        public LicenseController(ILicenseService service, ILogger<LicenseController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<IActionResult> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0)
        {
            return Ok(await _service.GetPageAsync(active, currentPage, pageSize));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync([FromBody] LicenseDTO entity)
        {
            return Ok(await _service.CreateAsync(entity));
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<IActionResult> UpdateAsync(int id, [FromBody] LicenseDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            return Ok(await _service.UpdateAsync(entity));
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }


        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SetActiveStatusAsync(int id, [FromBody] int status)
        {
            if (_service is IActiveStatusService)
            {
                return Ok(await (_service as IActiveStatusService).SetActiveStatusAsync(id, status == 1));
            }

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync([FromBody] NewLicenseDTO license)
        {
            return Ok(await _service.CreateAsync(license));
        }


        [HttpGet("{id}/branches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetBranchesAsync(int id)
        {
            return Ok(await _service.GetBranchesAsync(id));
        }

        [HttpPut("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddBranchAsync(int id, int branchId)
        {
            return Ok(await _service.AddBranchAsync(id, branchId));
        }

        [HttpDelete("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveBranchAsync(int id, int branchId)
        {
            return Ok(await _service.RemoveBranchAsync(id, branchId));
        }


        [HttpGet("branch/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLicensesBranchAsync(int branchId)
        {
            return Ok(await _service.GetLicensesBranchAsync(branchId));
        }
    }
}
