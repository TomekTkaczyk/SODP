using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Licenses;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/licenses")]
public class LicenseController : ApiBaseController
{
	public LicenseController(
		ISender sender,
		ILogger<LicenseController> logger,
		IMapper mapper) : base(sender, logger, mapper) { }


	[HttpGet("{id:int}", Name = "GetAsync")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
	[FromRoute] int id,
	CancellationToken cancellationToken = default)
	{
		var request = new GetLicenseRequest(id);

		return await HandleRequestAsync<GetLicenseRequest, ApiResponse<LicenseDTO>>(request, cancellationToken);
	}


	[HttpPost]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateLicenseRequest request,
		CancellationToken cancellationToken = default)
	{
		// var response = await _sender.Send(request, cancellationToken);
		await Task.CompletedTask;

		try
		{
			var result = CreatedAtAction(
				"Get",
				new { id = 17 },
				null);

			return result;
		}
		catch (Exception ex)
		{
			throw ex;
		} 
	}


	[HttpPatch("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UpdateAsync(
			int id,
			[FromBody] ChangeLicenseContentRequest request,
			CancellationToken cancellationToken = default)
	{
		if (request.Id != id)
		{
			return BadRequest();
		}

		return await HandleRequestAsync<ChangeLicenseContentRequest>(request, cancellationToken);
	}


	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new DeleteLicenseRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}

	[HttpGet("{id:int}/branches")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetLicenseWithBranchesAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetLicenseWithBranchesRequest(id);

		return await HandleRequestAsync<GetLicenseWithBranchesRequest, ApiResponse<LicenseDTO>>(request, cancellationToken);
	}


	[HttpPut("{id:int}/branches/{branchId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddBranchAsync(
		int id,
		int branchId,
		CancellationToken cancellationToken)
	{
		var request = new AddBranchToLicenseRequest(id, branchId);

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpDelete("{id:int}/branches/{branchId}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> RemoveBranchAsync(
		int id, 
		int branchId,
		CancellationToken cancellationToken)
	{
		var request = new RemoveBranchFromLicenseRequest(id, branchId);

		return await HandleRequestAsync(request, cancellationToken);
	}
}
