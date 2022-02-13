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
    [Route("api/v0_01/users")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _usersService;
        public UserController(IUserService usersService, ILogger<UserController> logger) : base(logger)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync()
        {
            var req = HttpContext.Request.Query;
            int.TryParse(req["page_number"], out int page_number);
            int.TryParse(req["page_size"], out int page_size);

            return Ok(await _usersService.GetAllAsync(currentPage: page_number, pageSize: page_size));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var serviceResponse = await _usersService.GetAsync(id);
            return serviceResponse.StatusCode switch
            {
                404 => StatusCode(serviceResponse.StatusCode),
                _ => Ok(serviceResponse),
            };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _usersService.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SetEnable(int id, [FromBody] UserDTO user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }

            return Ok(await _usersService.UpdateAsync(user));
        }

        [HttpPut("{id}/{enabled}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SetEnable(int id, [FromBody]int enabled)
        {
            return Ok(await _usersService.SetActiveStatusAsync(id, enabled==1));
        }
    }
}
