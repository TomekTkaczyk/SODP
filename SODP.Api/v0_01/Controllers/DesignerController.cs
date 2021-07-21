using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
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

        public DesignerController(IDesignerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int currentPage = 1, int pageSize = 15, bool? active = null)
        {
            return Ok(await _service.GetAllAsync(currentPage, pageSize, active));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DesignerDTO designer)
        {
            return Ok(await _service.CreateAsync(designer));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DesignerDTO designer)
        {
            if (id != designer.Id)
            {
                return BadRequest();
            }

            return Ok(await _service.UpdateAsync(designer));
        }
    }
}
