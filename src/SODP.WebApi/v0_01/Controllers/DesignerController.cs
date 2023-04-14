using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace SODP.WebApi.v0_01.Controllers
{
	[ApiController]
    [Route("api/v0_01/designers")]
    public class DesignerController : ActiveStatusController<Designer>
	{
        private readonly IDesignerService _service;

        public DesignerController(
            IDesignerService service, 
            ISender sender, 
            IMapper mapper, 
            ILogger<DesignerController> logger) 
            : base(sender, mapper, logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


		[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetPageAsync(
            bool? active, 
            string searchString = "", 
            int pageNumber = 1, 
            int pageSize = 0, 
            CancellationToken cancellationToken = default)
		{
            var request = new GetDesignersPageRequest(active, searchString, pageNumber, pageSize);
            
            return await HandleRequestAsync<GetDesignersPageRequest,ApiResponse<Page<DesignerDTO>>>(request, cancellationToken);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var request = new GetDesignerRequest(id);

            return await HandleRequestAsync<GetDesignerRequest,ApiResponse<DesignerDTO>>(request, cancellationToken);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateDesignerRequest request, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _sender.Send(request, cancellationToken);

				return CreatedAtAction(
					   nameof(GetAsync),
					   new { response.Value.Id },
					   response);
			}
			catch (DesignerConflictException ex)
            {
                return Conflict(ApiResponse.Failure(ex.Message,HttpStatusCode.Conflict));
            }
			catch (Exception ex)
            {
                return UnknowServerError(ex.Message);
            }
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<IActionResult> UpdateAsync(
            int id, 
            [FromBody] ChangeDesignerNameRequest request,
            CancellationToken cancellationToken = default)
        {
			if (id != request.Id)
			{
				return BadRequest(request);
			}
			
            return await HandleRequestAsync(request, cancellationToken);
		}


		[HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteDesignerRequest(id);

            return await HandleRequestAsync<DeleteDesignerRequest>(request, cancellationToken);
        }


        [HttpGet("{id}/licenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetWithLicensesAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
			var request = new GetDesignerWithDetailsRequest(id);

            return await HandleRequestAsync<GetDesignerWithDetailsRequest,ApiResponse<DesignerLicensesDTO>>(request, cancellationToken);
		}
	}
}
