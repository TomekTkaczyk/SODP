using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Shared.Results;
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
		ILogger<InvestorController> logger, 
		IMapper mapper) : base(sender, logger, mapper) { }

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
		if (pageNumber < 1)
		{
			return BadRequest(new Error("pageNumber is invalid."));
		}

		if (pageNumber > 1 && pageSize == 0)
		{
			return BadRequest(new Error("pageNumber and/or pageSize is invalid."));
		}

		var request = new GetInvestorsPageRequest(active, searchString, pageNumber, pageSize);

		return await HandleRequestAsync<GetInvestorsPageRequest, ApiResponse<Page<InvestorDTO>>>(request,cancellationToken);
	}


	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		int id,
		CancellationToken cancellationToken = default)
	{
		var request = new GetInvestorRequest(id);

		return await HandleRequestAsync<GetInvestorRequest,ApiResponse<InvestorDTO>>(request, cancellationToken);
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
		catch (ConflictException ex)
		{
			return Conflict(ApiResponse.Failure(ex.Message, HttpStatusCode.Conflict, new List<Error>()));
		}
		catch (Exception ex)
		{
			return UnknowServerError(ApiResponse.Failure(ex.Message, HttpStatusCode.InternalServerError, new List<Error>()));
		}
	}


	[HttpPatch("{id:int}")]
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
		CancellationToken cancellationToken = default)
	{
		var request = new DeleteInvestorRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}
}
