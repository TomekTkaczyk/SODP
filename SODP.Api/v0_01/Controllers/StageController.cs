using Microsoft.AspNetCore.Cors;
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
    [Route("api/v0_01/stages")]
    [EnableCors("SODPOriginsSpecification")]
    public class StageController : ApiControllerBase
    {
        private readonly IStageService _service;

        public StageController(IStageService service, ILogger<StageController> logger) : base(logger)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync(int currentPage = 1, int pageSize = 15, string searchString = "")
        {
            var response = await _service.GetAllAsync(currentPage, pageSize, searchString);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost("{sign}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<StageDTO>> Create(string sign, [FromBody] StageDTO stage)
        {
            if (sign != stage.Sign)
            {
                return BadRequest();
            }

            return Ok(await _service.CreateAsync(stage));
        }

        [HttpPut("{sign}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<StageDTO>> UpdateAsync(string sign, [FromBody] StageDTO stage)
        {
            if (sign != stage.Sign)
            {
                return BadRequest();
            }
            var response = await _service.UpdateAsync(stage);
            if (!response.Success)
            {
                return BadRequest(response);
            }

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



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
