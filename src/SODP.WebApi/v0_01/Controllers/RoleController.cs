using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Roles;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/roles")]
public class RoleController : ApiBaseController
{
	public RoleController(
		ISender sender,
		ILogger<RoleController> logger,
		IMapper mapper) : base(sender, logger, mapper) { }


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPageAsync(
		[FromQuery] GetRolesPageRequest request,
		CancellationToken cancellationToken = default)
	{
		return await HandleRequestAsync<GetRolesPageRequest, ApiResponse<Page<RoleDTO>>>(request, cancellationToken);
	}
}
