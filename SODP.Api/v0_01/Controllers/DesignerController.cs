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
    [Route("api/v0_01/designers")]
    public class DesignerController : ApiControllerBase
    {
        private readonly IDesignerService _service;

        public DesignerController(IDesignerService service, ILogger<DesignerController> logger) : base(logger)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync(int currentPage = 1, int pageSize = 15, bool? active = null)
        {
            return Ok(await _service.GetAllAsync(currentPage, pageSize, active));
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
        public async Task<IActionResult> CreateAsync([FromBody] DesignerDTO designer)
        {
            return Ok(await _service.CreateAsync(designer));
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DesignerDTO designer)
        {
            if (id != designer.Id)
            {
                return BadRequest();
            }

            return Ok(await _service.UpdateAsync(designer));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            return Ok(response);
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] int status)
        {
            return Ok(await _service.SetActiveStatusAsync(id, status == 1));
        }

        [HttpGet("{id}/licenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLicensesAsync(int id)
        {
            var response = await _service.GetLicensesAsync(id);

            return Ok(response);
        }

        [HttpPost("{id}/licences")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddLicence(int id, [FromBody] LicenseDTO licence)
        {
            return Ok(await _service.AddLicenceAsync(id, licence));
        }
    }
}
