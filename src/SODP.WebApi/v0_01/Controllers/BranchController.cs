using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Branches;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
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
		if (pageNumber < 1)
		{
			return BadRequest(new Error("pageNumber is invalid."));
		}

		if (pageNumber > 1 && pageSize == 0)
		{
			return BadRequest(new Error("pageNumber and/or pageSize is invalid."));
		}

		var request = new GetBranchesPageRequest(active, searchString, pageNumber, pageSize);

		return await HandleRequestAsync<GetBranchesPageRequest, ApiResponse<Page<BranchDTO>>>(request, cancellationToken);
	}


	[HttpGet("{id:int}")]
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
				response);
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


	[HttpDelete("{id:int}")]
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


	[HttpGet("{id:int}/licenses")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetLicensesAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetBranchWithLicensesRequest(id);

		return await HandleRequestAsync<GetBranchWithLicensesRequest, ApiResponse<BranchDTO>>(request, cancellationToken);
	}
}
