using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        protected readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService service, ILogger<RoleController> logger) : base()
        {
            _service = service;
            _logger = logger;
        }

    [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync()
        {
            var req = HttpContext.Request.Query;
            int.TryParse(req["page_number"], out int page_number);
            int.TryParse(req["page_size"], out int page_size);
            
            var result = await _service.GetPageAsync(currentPage: page_number, pageSize: page_size);

            return Ok(result);
        }
    }
}
