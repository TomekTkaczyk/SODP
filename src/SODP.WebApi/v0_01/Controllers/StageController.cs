using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Stages;
using SODP.Application.Queries.Stages;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Shared;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

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
			var error = Result.Failure(new Error("Stage.BadRequest", "pageNumber and/or pageSize is invalid."));
			return BadRequest(error.Error);
		}

		var query = new GetStagesPageQuery(active, searchString, pageNumber, pageSize);
		try
		{
			var response = await _sender.Send(query, cancellationToken);
			if (response.IsSuccess)
			{
				return Ok(response);
			}

			return GetActionResult(response);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var query = new GetStageByIdQuery(id);
		try
		{
			var response = await _sender.Send(query, cancellationToken);

			if (response.IsSuccess)
			{
				return Ok(response);
			}

			return GetActionResult(response);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateStageCommand command, 
		CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _sender.Send(command, cancellationToken);
			if (response.IsSuccess)
			{
				return CreatedAtAction(
						nameof(GetAsync),
						new { response.Value.Id },
						_mapper.Map<Stage, StageDTO>(response.Value));
			}

			return GetActionResult(response);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpPatch("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> ChangeNameAsync(
		int id,
		[FromBody] ChangeStageNameCommand command,
		CancellationToken cancellationToken = default)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}

		try
		{
			return GetActionResult(await _sender.Send(command, cancellationToken));
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
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
		var command = new DeleteStageCommad(id);
		try
		{
			return GetActionResult(await _sender.Send(command, cancellationToken));
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}
}
