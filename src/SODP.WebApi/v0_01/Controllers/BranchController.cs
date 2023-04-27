using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Branches;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Shared;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

[ApiController]
[Route("/api/v0_01/branches")]
public class BranchController : ActiveStatusController<Branch>
{
	public BranchController(
		ISender sender,
		ILogger<BranchController> logger,
		IMapper mapper)
		: base(sender, logger, mapper) { }


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
		var request = new GetBranchesPageRequest(active, searchString, pageNumber, pageSize);

		return await HandleRequestAsync<GetBranchesPageRequest,ApiResponse<Page<BranchDTO>>>(request, cancellationToken);
	}


	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetBranchRequest(id);
		
		return await HandleRequestAsync<GetBranchRequest, ApiResponse<BranchDTO>>(request, cancellationToken);
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateBranchRequest request,
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
		[FromBody] UpdateBranchRequest request,
		CancellationToken cancellationToken)
	{
		if (id != request.Id)
		{
			return BadRequest();
		}

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new DeleteBranchRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpGet("{id}/licenses")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetLicensesAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var query = new GetBranchWithLicensesRequest(id);
		try
		{
			var branch = await _sender.Send(query, cancellationToken);

			return Ok(ApiResponse.Success(_mapper.Map<BranchDTO>(branch)));
		}
		catch (NotFoundException ex)
		{
			return NotFound(ApiResponse.Failure(ex.Message, HttpStatusCode.NotFound));
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}
}
