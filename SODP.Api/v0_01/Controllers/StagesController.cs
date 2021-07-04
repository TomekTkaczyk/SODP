using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/stages")]
    public class StagesController : ControllerBase
    {
        private readonly IStagesService _stagesService;

        public StagesController(IStagesService stagesService)
        {
            _stagesService = stagesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // sample code with id from token
            //var claim = User.Claims.FirstOrDefault(x => x.Type == "id");
            //if((claim == null) || !int.TryParse(claim.Value, out int userId))
            //{
            //    return BadRequest();
            //}
            return Ok(await _stagesService.GetAllAsync(1, 15));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _stagesService.GetAsync(id));
        }

        [HttpPut("{sign}")]
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
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _stagesService.DeleteAsync(id));
        }
    }
}
