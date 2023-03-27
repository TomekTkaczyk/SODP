using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Investors;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Shared.DTO;
using SODP.Domain.Shared;
using System.Threading.Tasks;
using SODP.Application.Queries.Investors;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SODP.Application.ValueObjects;
using SODP.Domain.ValueObjects;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/investors")]
public class InvestorController : ApiControllerBase
{
	private readonly ISender _sender;
	private readonly IMapper _mapper;

	public InvestorController(ISender sender, ILogger<InvestorController> logger, IMapper mapper)
			: base(logger)
	{
		_sender = sender;
		_mapper = mapper;
	}

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
		if(pageSize == 0 && pageNumber != 1)
		{
			var error = Result.Failure(new Error("Investor.BadRequest", "pageNumber and/or pageSize is invalid."));
			return BadRequest(error.Error);
		}

		var query = new GetInvestorsPageQuery(active, searchString, pageNumber, pageSize);

		var response = await _sender.Send(query, cancellationToken);

		return response.IsSuccess 
			? Ok(response.Value) 
			: NotFound(response.Error);
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

		var response = await _sender.Send(query, cancellationToken);

		return response.IsSuccess 
			? Ok(response.Value) 
			: NotFound(response.Error);
	}


	[HttpPost]
	[ProducesResponseType(typeof(Result<InvestorDTO>), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateInvestorCommand command, 
		CancellationToken cancellationToken = default)
	{
		var result = await _sender.Send(command, cancellationToken);

		return CreatedAtAction(
			nameof(GetAsync), 
			new { result.Value.Id }, 
			_mapper.Map<Investor, InvestorValueObject>(result.Value));
	}


	[HttpPut("{id}")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UpdateAsync(
		int id, 
		[FromBody] ChangeInvestorNameCommand command, 
		CancellationToken cancellationToken = default)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}
		var result = await _sender.Send(command, cancellationToken);

		return result.IsSuccess 
			? NoContent() 
			: NotFound();
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

		var result = await _sender.Send(command, cancellationToken);

		return result.IsSuccess 
			? NoContent() 
			: NotFound(result.Error);
	}


	[HttpPatch("{id}/status")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetActiveStatusAsync(
		int id,
		[FromBody] SetActiveStatusCommand command, 
		CancellationToken cancellationToke = default)
	{
		var result = await _sender.Send(command, cancellationToke);

		return result.IsSuccess 
			? NoContent() 
			: NotFound();
	}

}
