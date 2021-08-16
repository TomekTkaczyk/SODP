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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DesignerDTO designer)
        {
            return Ok(await _service.CreateAsync(designer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DesignerDTO designer)
        {
            if (id != designer.Id)
            {
                return BadRequest();
            }

            return Ok(await _service.UpdateAsync(designer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}/status/{status}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] int status)
        {
            return Ok(await _service.SetActiveStatusAsync(id, status == 1));
        }

        [HttpGet("{id}/licenses")]
        public async Task<IActionResult> GetLicensesAsync(int id)
        {
            var response = await _service.GetLicensesAsync(id);

            return Ok(response);
        }

        [HttpPost("{id}/licences")]
        public async Task<IActionResult> AddLicence(int id, [FromBody] LicenseDTO licence)
        {

            return Ok(await _service.AddLicenceAsync(id, licence));
        }
    }
}
