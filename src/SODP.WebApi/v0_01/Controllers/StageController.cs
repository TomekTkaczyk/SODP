using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Parts;
using SODP.Application.API.Requests.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

[ApiController]
[Route("api/v0_01/stages")]
public class StageController : ActiveStatusController<Stage>
{
	public StageController(
		ISender sender,
		ILogger<StageController> logger,
		IMapper mapper) : base(sender, logger, mapper) { }


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPageAsync(
		[FromQuery] GetStagesPageRequest request,
		CancellationToken cancellationToken = default)
	{
		if (request.PageNumber < 1)
		{
			return BadRequest(new Error("pageNumber is invalid."));
		}

		if (request.PageNumber > 1 && request.PageSize == 0)
		{
			return BadRequest(new Error("pageNumber and/or pageSize is invalid."));
		}

		return await HandleRequestAsync<GetStagesPageRequest,ApiResponse<Page<StageDTO>>>(request, cancellationToken);
	}


	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken = default)
	{
		var request = new GetStageRequest(id);

		return await HandleRequestAsync<GetStageRequest,ApiResponse<StageDTO>>(request, cancellationToken);
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateStageRequest request, 
		CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _sender.Send(request, cancellationToken);
			return CreatedAtAction(
					nameof(GetAsync),
					new { id = response.Value },
					null);
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
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateByIdAsync(
		[FromRoute] int id,
		[FromBody] UpdateStageByIdRequest request,
		CancellationToken cancellationToken = default)
	{
		if (id != request.Id)
		{
			return BadRequest();
		}

		return await HandleRequestAsync<UpdateStageByIdRequest, ApiResponse>(request, cancellationToken);
	}


	[HttpPut()]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateBySignAsync(
		[FromBody] UpdateStageBySignRequest request,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(request.Sign))
		{
			return BadRequest();
		}

		try
		{
			return await HandleRequestAsync<UpdateStageBySignRequest, ApiResponse>(request, cancellationToken);
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
		var request = new DeleteStageRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}
}
