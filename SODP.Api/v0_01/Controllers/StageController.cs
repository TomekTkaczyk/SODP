using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/stages")]
    public class StageController : ApiControllerBase
    {
        private readonly IStageService _stagesService;

        public StageController(IStageService stagesService, ILogger<StageController> logger) : base(logger)
        {
            _stagesService = stagesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync(int currentPage = 1, int pageSize = 15)
        {
            // sample code with id from token
            //var claim = User.Claims.FirstOrDefault(x => x.Type == "id");
            //if((claim == null) || !int.TryParse(claim.Value, out int userId))
            //{
            //    return BadRequest();
            //}
            return Ok(await _stagesService.GetAllAsync(currentPage, pageSize));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _stagesService.GetAsync(id));
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

            return Ok(await _stagesService.CreateAsync(stage));
        }

        [HttpPut("{sign}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<StageDTO>> Update(string sign, [FromBody] StageDTO stage)
        {
            if (sign != stage.Sign)
            {
                return BadRequest();
            }
            var response = await _stagesService.UpdateAsync(stage);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _stagesService.DeleteAsync(id));
        }
    }
}
