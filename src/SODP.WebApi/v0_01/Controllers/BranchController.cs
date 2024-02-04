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
		ILogger<BranchController> logger) : base(sender, logger) { }


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPageAsync(
		[FromQuery] GetBranchesPageRequest request,
		CancellationToken cancellationToken = default)
	{
		if (request.PageNumber < 1)
		{
			return BadRequest(new Error("pageNumber is invalid."));
		}

		if (request.PageNumber > 1 && request.PageSize == 0)
		{
			return BadRequest(new Error("pageNumber and/or pageSize is invalid."));
		}

		return await HandleRequestAsync<GetBranchesPageRequest, ApiResponse<Page<BranchDTO>>>(
			request, 
			cancellationToken);
	}


	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken = default)
	{
		return await HandleRequestAsync<GetBranchRequest, ApiResponse<BranchDTO>>(
			new GetBranchRequest(id), 
			cancellationToken);
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateBranchRequest request,
		CancellationToken cancellationToken = default)
	{
		var response = await _sender.Send(request, cancellationToken);

		return CreatedAtAction(
			nameof(GetAsync),
			new { response.Value },
			response);
	}


	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateAsync(
		int id,
		[FromBody] UpdateBranchRequest request,
		CancellationToken cancellationToken = default)
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
		[FromRoute] int id, 
		CancellationToken cancellationToken = default)
	{
		return await HandleRequestAsync(new DeleteBranchRequest(id), cancellationToken);
	}


	[HttpGet("{id:int}/details")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetLicensesAsync(
		[FromRoute] int id, 
		CancellationToken cancellationToken = default)
	{
		return await HandleRequestAsync<GetBranchDetailsRequest, ApiResponse<BranchDTO>>(
			new GetBranchDetailsRequest(id), 
			cancellationToken);
	}
}
