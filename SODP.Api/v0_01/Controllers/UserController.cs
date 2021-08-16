using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;
        public UserController(IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var req = HttpContext.Request.Query;
            int.TryParse(req["page_number"], out int page_number);
            int.TryParse(req["page_size"], out int page_size);

            return Ok(await _usersService.GetAllAsync(currentPage: page_number, pageSize: page_size));
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _usersService.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SetEnable(int id, [FromBody] UserDTO user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }

            return Ok(await _usersService.UpdateAsync(user));
        }

        [HttpPut("{id}/{enabled}")]
        public async Task<IActionResult> SetEnable(int id, [FromBody]int enabled)
        {
            return Ok(await _usersService.SetActiveStatusAsync(id, enabled==1));
        }
    }
}
