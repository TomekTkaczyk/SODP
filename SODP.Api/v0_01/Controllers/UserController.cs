using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
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

            var aaa = await _usersService.GetAllAsync(currentPage: page_number, pageSize: page_size);

            return Ok(aaa);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _usersService.GetAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _usersService.DeleteAsync(id));
        }

        [HttpPut("{id}/{enabled}")]
        public async Task<IActionResult> SetEnable(int id, [FromBody]int enabled)
        {
            return Ok(await _usersService.SetEnable(id, enabled==1));
        }

    }
}
