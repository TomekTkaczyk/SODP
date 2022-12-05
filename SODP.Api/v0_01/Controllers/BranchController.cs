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
    public class BranchController : ApiControllerBase
    {
        private readonly IBranchService _service;

        public BranchController(IBranchService service, ILogger<ApiControllerBase> logger) :base(logger)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync(bool? ActiveOnly)
        {
            return Ok(await _service.GetAllAsync(ActiveOnly));
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
        public async Task<ActionResult<BranchDTO>> Create([FromBody] BranchDTO branch)
        {
            var response = await _service.CreateAsync(branch);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BranchDTO>> Update(int id, [FromBody] BranchDTO branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            return Ok(await _service.UpdateAsync(branch));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

        [HttpPatch("{Id}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] int status)
        {
            return Ok(await _service.SetActiveStatusAsync(id, status == 1));
        }


        [HttpGet("{id}/designers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLicensesAsync(int id)
        {
            return Ok(await _service.GetLicensesAsync(id));
        }
    }
}
