using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Parts;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.WebApi.v0_01.Controllers;

[ApiController]
[Route("api/v0_01/parts")]
public class PartController : ActiveStatusController<Part>
{
	public PartController(
		ISender sender,
		ILogger<PartController> logger,
		IMapper mapper) : base(sender, logger, mapper) { }


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
		var request = new GetPartsPageRequest(active, searchString, pageNumber, pageSize);

		return await HandleRequestAsync<GetPartsPageRequest, ApiResponse<Page<PartDTO>>>(request, cancellationToken);
	}


	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetPartRequest(id);

		return await HandleRequestAsync<GetPartRequest, ApiResponse<ProjectPartDTO>>(request, cancellationToken);
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreatePartRequest request,
		CancellationToken cancellationToken)
	{
		try
		{
			var response = await _sender.Send(request, cancellationToken);
			return CreatedAtAction(
				nameof(GetAsync),
				new { response.Value.Id },
				response.Value);
		}
		catch (ConflictException ex)
		{
			return Conflict(ex.Message);
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}


	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateAsync(
		int id,
		[FromBody] UpdatePartRequest request,
		CancellationToken cancellationToken)
	{
		if (id != request.Id)
		{
			return BadRequest();
		}

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new DeletePartRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}
}
