using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Parts;
using SODP.Application.API.Requests.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;

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
		if (pageNumber < 1)
		{
			return BadRequest(new Error("pageNumber is invalid."));
		}

		if (pageNumber > 1 && pageSize == 0)
		{
			return BadRequest(new Error("pageNumber and/or pageSize is invalid."));
		}

		var request = new GetPartsPageRequest(active, searchString, pageNumber, pageSize);

		return await HandleRequestAsync<GetPartsPageRequest, ApiResponse<Page<PartDTO>>>(request, cancellationToken);
	}


	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		int id,
		CancellationToken cancellationToken = default)
	{
		var request = new GetPartRequest(id);

		return await HandleRequestAsync<GetPartRequest, ApiResponse<PartDTO>>(request, cancellationToken);
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreatePartRequest request,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _sender.Send(request, cancellationToken);
			return CreatedAtAction(
				nameof(GetAsync),
				new { id = response.Value },
				response);
		}
		catch (ConflictException ex)
		{
			return Conflict(
				ApiResponse.Failure(
					ex.Message, 
					HttpStatusCode.Conflict, 
					new List<Error>()));
		}
		catch (Exception ex)
		{
			return UnknowServerError(
				ApiResponse.Failure(
					ex.Message, 
					HttpStatusCode.InternalServerError, 
					new List<Error>()));
		}
	}


	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateByIdAsync(
		[FromRoute] int id,
		[FromBody] UpdatePartByIdRequest request,
		CancellationToken cancellationToken = default)
	{
		if (id != request.Id)
		{
			return BadRequest();
		}

		return await HandleRequestAsync<UpdatePartByIdRequest, ApiResponse>(request, cancellationToken);
	}


	[HttpPut()]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateBySignAsync(
		[FromBody] UpdatePartBySignRequest request,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(request.Sign))
		{
			return BadRequest();
		}

		try
		{
			return await HandleRequestAsync<UpdatePartBySignRequest, ApiResponse>(request, cancellationToken);
		}
		catch (DomainException ex)
		{
			return NotFound(
				ApiResponse.Failure(
					ex.Message,
					HttpStatusCode.NotFound,
					new List<Error>()));
		}
		catch (Exception ex)
		{
			return UnknowServerError(
				ApiResponse.Failure(
					ex.Message,
					HttpStatusCode.InternalServerError,
					new List<Error>()));
		}

	}


	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken = default)
	{
		var request = new DeletePartRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}
}
