using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Users;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/users")]
public class UserController : ApiBaseController
{

	public UserController(
		ISender sender,
		ILogger<UserController> logger) : base(sender, logger) { }


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public virtual async Task<IActionResult> GetPageAsync(
		[FromQuery] GetUsersPageRequest request,
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
