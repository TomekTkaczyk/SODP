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
    public class StageController : ApiControllerBase<StageDTO>
    {
        public StageController(IStageService service, ILogger<StageController> logger) : base(service, logger) { }

        
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
    }
}
