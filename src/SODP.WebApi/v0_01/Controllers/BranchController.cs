using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Shared;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.WebApi.v0_01.Controllers;

[ApiController]
[Route("/api/v0_01/branches")]
public class BranchController : ActiveStatusController<Branch>
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

		var query = new GetBranchesPageRequest(active, searchString, pageNumber, pageSize);
		try
		{
			var branches = await _sender.Send(query, cancellationToken);
			return Ok(ApiResponse.Success(_mapper.Map<Page<BranchDTO>>(branches)));
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
		CancellationToken cancellationToken)
	{
		var query = new GetBranchByIdRequest(id);
		try
		{
			var branch = await _sender.Send(query, cancellationToken);
			return Ok(ApiResponse.Success<BranchDTO>(_mapper.Map<BranchDTO>(branch)));
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
		[FromBody] CreateBranchRequest command,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var branch = await _sender.Send(command, cancellationToken);
			return CreatedAtAction(
				nameof(GetAsync),
				new { branch.Id },
				_mapper.Map<BranchDTO>(branch));
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
		[FromBody] UpdateBranchRequest command,
		CancellationToken cancellationToken)
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
		catch (ConflictException ex)
		{
			return Conflict(ex.Message);
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
		CancellationToken cancellationToken)
	{
		var command = new DeleteBranchRequest(id);
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


	[HttpGet("{id}/licenses")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetLicensesAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var query = new GetBranchByIdWithLicensesRequest(id);
		try
		{
			var branch = await _sender.Send(query, cancellationToken);

			return Ok(ApiResponse.Success(_mapper.Map<BranchDTO>(branch)));
		}
		catch (NotFoundException ex)
		{
			return NotFound(ApiResponse.Failure(ex.Message));
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}
}
