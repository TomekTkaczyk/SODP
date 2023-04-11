using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SODP.Application.Commands.Designers;
using SODP.Application.Queries.Designers;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading.Tasks;

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
            var query = new GetDesignersPageQuery(active, searchString, pageNumber, pageSize);
            try
            {
                var designers = await _sender.Send(query, cancellationToken);
                return Ok(ApiResponse.Success(_mapper.Map<Page<DesignerDTO>>(designers)));
            }
            catch (Exception ex)
            {
                return UnknowServerError(ex);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(
            int id, 
            CancellationToken cancellationToken = default)
        {
            var query = new GetDesignerByIdQuery(id);
            try
            {
                var designer = await _sender.Send(query, cancellationToken);
                return Ok(ApiResponse.Success(_mapper.Map<DesignerDTO>(designer)));
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return UnknowServerError(ex);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateDesignerCommand command, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var designer = await _sender.Send(command, cancellationToken);
                var apiResponse = ApiResponse.Success(_mapper.Map<DesignerDTO>(designer));

				return CreatedAtAction(
					   nameof(GetAsync),
					   new { apiResponse.Value.Id },
					   apiResponse);
			}
			catch (DesignerConflictException ex)
            {
                return Conflict(ex.Message);
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
            [FromBody] ChangeDesignerNameCommand command,
            CancellationToken cancellationToken)
        {
			if (id != command.Id)
			{
				return BadRequest(command);
			}
			
            try
			{
				var response = await _sender.Send(command, cancellationToken);
				return NoContent();
			}
			catch (ConflictException ex)
			{
				return Conflict(ApiResponse.Failure(ex.Message));
			}
			catch (NotFoundException ex)
			{
				return NotFound(ApiResponse.Failure(ex.Message));
			}
			catch (Exception ex)
			{
				return UnknowServerError(ex);
			}
		}


		[HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteDesignerCommand(id);
            try
            {
                await _sender.Send(command, cancellationToken);
                return NoContent();
            }
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
            {
                return UnknowServerError(ex.Message);
            }
        }


        [HttpGet("{id}/licenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetDesignerWithLicensesAsync(
            int id,
            CancellationToken cancellationToken)
        {
			var query = new GetDesignerWithDetailsQuery(id);
			try
			{
				var designer = await _sender.Send(query, cancellationToken);

                var response = new DesignerLicensesDTO(
                    _mapper.Map<DesignerDTO>(designer),
                    _mapper.Map<ICollection<LicenseDTO>>(designer.Licenses));

				return Ok(ApiResponse.Success(response));
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return UnknowServerError(ex);
			}
		}

        [HttpPost("{id}/licenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddLicense(int id, [FromBody] LicenseDTO license)
        {
            return Ok(await _service.AddLicenseAsync(id, license));
        }
    }
}
