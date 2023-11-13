using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Users;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/users")]
public class UserController : ApiControllerBase
{

	public UserController(
		ISender sender,
		ILogger<UserController> logger,
		IMapper mapper) : base(sender, logger, mapper) { }


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> GetPageAsync(
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

		var request = new GetUsersPageRequest(active, searchString, pageNumber, pageSize);
		
		return await HandleRequestAsync<GetUsersPageRequest, ApiResponse<Page<UserDTO>>>(request, cancellationToken);
	}


	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		var request = new GetUserRequest(id);

		return await HandleRequestAsync<GetUserRequest, ApiResponse<UserDTO>>(request, cancellationToken);
	}


	[HttpPatch("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> UpdateAsync(
		int id, 
		[FromBody] UpdateUserRequest request,
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
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		var request = new DeleteUserRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}
}
