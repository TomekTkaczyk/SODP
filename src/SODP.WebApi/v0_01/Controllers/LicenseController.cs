using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Branches;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Services;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace SODP.WebApi.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/licenses")]
    public class LicenseController : ApiControllerBase
    {
        private readonly ILicenseService _service;

        public LicenseController(
            ILicenseService service, 
            ISender sender, 
            IMapper mapper, 
            ILogger<LicenseController> logger) : base(sender, mapper, logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetAsync(
			int id,
			CancellationToken cancellationToken = default)
		{
			var request = new GetLicenseRequest(id);

			return await HandleRequestAsync<GetLicenseRequest, ApiResponse<LicenseDTO>>(request, cancellationToken);
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> CreateAsync(
	        [FromBody] CreateLicenseRequest request,
	        CancellationToken cancellationToken = default)
		{
			return await HandleRequestAsync<CreateLicenseRequest, ApiResponse<LicenseDTO>>(request, cancellationToken);
		}


		[HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateAsync(
            int id, 
            [FromBody] ChangeLicenseContentRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request.Id != id)
            {
                return BadRequest();
            }

            return await HandleRequestAsync<ChangeLicenseContentRequest>(request, cancellationToken);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var request = new DeleteLicenseRerquest(id);

            return await HandleRequestAsync(request, cancellationToken);
        }

        [HttpGet("{id}/branches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetWithBranchesAsync(int id)
        {
            return Ok(await _service.GetBranchesAsync(id));
        }


        [HttpPut("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddBranchAsync(int id, int branchId)
        {
            return Ok(await _service.AddBranchAsync(id, branchId));
        }


        [HttpDelete("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveBranchAsync(int id, int branchId)
        {
            return Ok(await _service.RemoveBranchAsync(id, branchId));
        }


        [HttpGet("branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLicensesBranchAsync(int branchId)
        {
            return Ok(await _service.GetLicensesBranchAsync(branchId));
        }
    }
}
