using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/licenses")]
    public class LicenseController : ApiControllerBase
    {
        private readonly ILicenseService _service;

        public LicenseController(ILicenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpGet("{id}/branches")]
        public async Task<IActionResult> GetBranchesAsync(int id)
        {
            return Ok(await _service.GetBranchesAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] LicenseDTO license)
        {
            return Ok(await _service.CreateAsync(license));
        }

        [HttpPut("{id}/branches/{branchId}")]
        public async Task<IActionResult> AddBranchAsync(int id, int branchId)
        {

            return Ok(await _service.AddBranchAsync(id, branchId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] LicenseDTO license)
        {
            return Ok(await _service.UpdateAsync(license));
        }

        [HttpDelete("{id}/branches/{branchId}")]
        public async Task<IActionResult> RemoveBranchAsync(int id, int branchId)
        {
            return Ok(await _service.RemoveBranchAsync(id, branchId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
