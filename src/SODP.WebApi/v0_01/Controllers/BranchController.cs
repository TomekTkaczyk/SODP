using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Branches;
using SODP.Application.Commands.Investors;
using SODP.Application.Commands.Stages;
using SODP.Application.Queries.Branczes;
using SODP.Application.Queries.Investors;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Shared;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.WebApi.v0_01.Controllers;

[ApiController]
[Route("/api/v0_01/branches")]
public class BranchController : ApiControllerBase
{
	private readonly IBranchService _service;

	public BranchController(
		IBranchService service,
		ISender sender,
		IMapper mapper,
		ILogger<BranchController> logger)
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
		if (pageSize == 0 && pageNumber != 1)
		{
			var error = Result.Failure(new Error("Branch.BadRequest", "pageNumber and/or pageSize is invalid."));
			return BadRequest(error.Error);
		}

		var query = new GetBranchesPageQuery(active, searchString, pageNumber, pageSize);
		try
		{
			var response = await _sender.Send(query, cancellationToken);

			if(response.IsSuccess) 
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
		var query = new GetBranchByIdQuery(id);
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
		[FromBody] CreateBranchCommand command,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await _sender.Send(command, cancellationToken);
			if (result.IsSuccess)
			{
				return CreatedAtAction(
					nameof(GetAsync),
					new { result.Value.Id },
					_mapper.Map<Branch, BranchDTO>(result.Value));
			}

			return GetActionResult(result);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateAsync(
		int id, 
		[FromBody] UpdateBranchCommand command,
		CancellationToken cancellationToken)
	{
		if(id != command.Id)
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
		var command = new DeleteBranchCommand(id);
		try
		{
			return GetActionResult(await _sender.Send(command, cancellationToken));
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpPatch("{id}/status/{status}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetActiveStatusAsync(
		int id, 
		int status)
	{
		if (_service is IActiveStatusService)
		{
			return Ok(await (_service as IActiveStatusService).SetActiveStatusAsync(id, status == 1));
		}

		return BadRequest();
	}


	[HttpGet("{id}/designers")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetLicensesAsync(int id)
	{
		return Ok(await (_service as IBranchService).GetLicensesAsync(id));
	}
}
