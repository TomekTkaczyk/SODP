using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Investors;
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
			return BadRequest(ApiResponse.Failure("pageNumber and/or pageSize is invalid.",HttpStatusCode.BadRequest));
		}

		var query = new GetInvestorsPageRequest(active, searchString, pageNumber, pageSize);
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
		var query = new GetInvestorByIdRequest(id);
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
		[FromBody] CreateInvestorRequest request,
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
		catch (InvestorExistException ex)
		{
			return Conflict(ApiResponse.Failure(ex.Message, HttpStatusCode.Conflict));
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
		[FromBody] ChangeInvestorNameRequest request,
		CancellationToken cancellationToken = default)
	{
		if (id != request.Id)
		{
			return BadRequest(request);
		}

		try
		{
			var response = await _sender.Send(request, cancellationToken);
			return NoContent();
		}
		catch (InvestorExistException ex)
		{
			return Conflict(ApiResponse.Failure(ex.Message, HttpStatusCode.Conflict));
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
		var command = new DeleteInvestorRequest(id);
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
