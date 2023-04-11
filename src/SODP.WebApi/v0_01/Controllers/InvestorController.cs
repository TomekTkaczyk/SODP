using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Investors;
using SODP.Application.Queries.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Shared;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/investors")]
public class InvestorController : ActiveStatusController<Investor>
{
	public InvestorController(
		ISender sender,
		IMapper mapper,
		ILogger<InvestorController> logger)
		: base(sender, mapper, logger) { }

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPageAsync(
		bool? active,
		string searchString,
		int pageNumber = 1,
		int pageSize = 0,
		CancellationToken cancellationToken = default)
	{
		if (pageSize == 0 && pageNumber != 1)
		{
			return BadRequest(ApiResponse.Failure("pageNumber and/or pageSize is invalid."));
		}

		var query = new GetInvestorsPageQuery(active, searchString, pageNumber, pageSize);
		try
		{
			var investors = await _sender.Send(query, cancellationToken);

			return Ok(ApiResponse.Success(_mapper.Map<Page<InvestorDTO>>(investors)));
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
		var query = new GetInvestorByIdQuery(id);
		try
		{
			var investor = await _sender.Send(query, cancellationToken);
			return Ok(ApiResponse.Success(_mapper.Map<InvestorDTO>(investor)));
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}


	[HttpPost]
	[ProducesResponseType(typeof(ApiResponse<InvestorDTO>), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateInvestorCommand command,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var investor = await _sender.Send(command, cancellationToken);
			var apiResponse = ApiResponse.Success(_mapper.Map<InvestorDTO>(investor));

			return CreatedAtAction(
				   nameof(GetAsync),
				   new { apiResponse.Value.Id },
				   apiResponse);
		}
		catch (ConflictException ex)
		{
			return Conflict(ApiResponse.Failure(ex.Message));
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}


	[HttpPatch("{id}")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> ChangeNameAsync(
		int id,
		[FromBody] ChangeInvestorNameCommand command,
		CancellationToken cancellationToken = default)
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
		CancellationToken cancellationToken = default)
	{
		var command = new DeleteInvestorCommand(id);
		try
		{
			await _sender.Send(command, cancellationToken);
			return NoContent();
		}
		catch (InvestorNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (UnknowDeleteException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}
}
