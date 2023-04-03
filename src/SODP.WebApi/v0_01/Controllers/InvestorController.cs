using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Investors;
using SODP.Application.Queries.Investors;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Shared;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/investors")]
public class InvestorController : ActiveStatusController
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
			var error = Result.Failure(new Error("Investor.BadRequest", "pageNumber and/or pageSize is invalid."));
			return BadRequest(error.Error);
		}

		var query = new GetInvestorsPageQuery(active, searchString, pageNumber, pageSize);
		try
		{
			var response = await _sender.Send(query, cancellationToken);

			return response.IsSuccess
				? Ok(response)
				: NotFound(response.Errors);
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
		CancellationToken cancellationToken = default)
	{
		var query = new GetInvestorByIdQuery(id);
		try
		{
			var response = await _sender.Send(query, cancellationToken);

			return response.IsSuccess
				? Ok(response)
				: NotFound(response.Errors);

		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpPost]
	[ProducesResponseType(typeof(Result<InvestorDTO>), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateInvestorCommand command,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await _sender.Send(command, cancellationToken);
			return result.IsSuccess
				? CreatedAtAction(
					nameof(GetAsync),
					new { result.Value.Id },
					_mapper.Map<Investor, InvestorDTO>(result.Value))
				: Conflict(result.Errors);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
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
			var result = await _sender.Send(command, cancellationToken);
			return result.IsSuccess
				? NoContent()
				: Conflict(result);
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
		CancellationToken cancellationToken = default)
	{
		var command = new DeleteInvestorCommand(id);
		try
		{
			var result = await _sender.Send(command, cancellationToken);
			return result.IsSuccess
				? NoContent()
				: NotFound(result.Errors);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}
}
