using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Shared;
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
		IMapper mapper,
		ILogger<StageController> logger)
		: base(sender, mapper, logger) { }


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
		if (pageSize == 0 && pageNumber != 1)
		{
			var error = Result.Failure(new Error("pageNumber and/or pageSize is invalid."));
			return BadRequest(error.Error);
		}

		var query = new GetStagesPageRequest(active, searchString, pageNumber, pageSize);
		try
		{
			var stages = await _sender.Send(query, cancellationToken);
			return Ok(ApiResponse.Success(_mapper.Map<Page<StageDTO>>(stages)));
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
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		var query = new GetStageRequest(id);
		try
		{
			var stage = await _sender.Send(query, cancellationToken);
			return Ok(ApiResponse.Success(_mapper.Map<BranchDTO>(stage)));
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
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateStageRequest request, 
		CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _sender.Send(request, cancellationToken);
			return CreatedAtAction(
					nameof(GetAsync),
					new { response.Value.Id },
					response.Value);
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}


	[HttpPatch("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> ChangeNameAsync(
		[FromRoute] int id,
		[FromBody] ChangeStageNameRequest command,
		CancellationToken cancellationToken = default)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}

		try
		{
			await _sender.Send(command, cancellationToken);
			return NoContent();
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
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		var command = new DeleteStageRequest(id);
		try
		{
			await _sender.Send(command, cancellationToken);
			return NoContent();
		}
		catch (ResourceIsInUseException ex)
		{
			return Conflict(ApiResponse.Failure(ex.Message, HttpStatusCode.Conflict));
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}
}
