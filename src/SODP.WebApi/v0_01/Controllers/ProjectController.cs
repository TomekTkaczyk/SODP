using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Services;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/projects")]
public class ProjectController : ApiControllerBase
{
	public ProjectController(
		ISender sender,
		ILogger<ProjectController> logger,
		IMapper mapper) : base(sender, logger, mapper) { }


	#region Project

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPageAsync(
		[FromQuery] GetProjectsPageRequest request,
		CancellationToken cancellationToken = default)
	{
		if (request.PageSize == 0 && request.PageNumber != 1)
		{
			return BadRequest($"pageNumber and/or pageSize is invalid.");
		}

		var result =  await HandleRequestAsync<GetProjectsPageRequest, ApiResponse<Page<ProjectDTO>>>(request, cancellationToken);

		return result;
	}


	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken = default)
	{
		return await HandleRequestAsync<GetProjectRequest, ApiResponse<ProjectDTO>>(
			new GetProjectRequest(id), 
			cancellationToken);
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateProjectRequest request,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _sender.Send(request, cancellationToken);

			return CreatedAtAction(
				nameof(GetAsync),
				new { id = response.Value },
				null);
		}
		catch (ConflictException ex)
		{
			return Conflict(
				ApiResponse.Failure(
					ex.Message,
					HttpStatusCode.Conflict,
					new List<Error>()));
		}
		catch (DomainException ex)
		{
			return BadRequest(
				ApiResponse.Failure(
					ex.Message,
					HttpStatusCode.BadRequest,
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


	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken = default)
	{
		return await HandleRequestAsync(
			new DeleteProjectRequest(id), 
			cancellationToken);
	}


	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Update(
		[FromRoute] int id,
		[FromBody] UpdateProjectRequest request,
		CancellationToken cancellationToken)
	{
		if ((id < 1) || (id != request.Id))
		{
			return BadRequest();
		}

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpPatch("{id:int}/archive")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Archive(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(
			new ArchiveProjectRequest(id), 
			cancellationToken);
	}


	[HttpPatch("{id:int}/restore")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Restore(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(
			new RestoreProjectRequest(id), 
			cancellationToken);
	}


	[HttpPatch("{id:int}/investor")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetInvestorAsync(
		[FromRoute] int id,
		[FromBody] SetInvestorRequest request,
		CancellationToken cancellationToken)
	{
		if (id != request.Id)
		{
			return BadRequest();
		}

		return await HandleRequestAsync(request, cancellationToken);
	}

	#endregion

	#region Parts

	[HttpGet("{id:int}/details")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetWithDetailsAsync(
	[FromRoute] int id,
	CancellationToken cancellationToken)
	{
		return await HandleRequestAsync<GetProjectWithDetailsRequest, ApiResponse<ProjectDTO>>(
			new GetProjectWithDetailsRequest(id), 
			cancellationToken);
	}


	[HttpGet("parts/{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync<GetProjectPartRequest, ApiResponse<ProjectPartDTO>>(
			new GetProjectPartRequest(id), 
			cancellationToken);
	}


	[HttpPost("{id:int}/parts")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddPartAsync(
		[FromRoute] int id,
		[FromBody] AddPartRequest request,
		CancellationToken cancellationToken)
	{
		if (id != request.Id)
		{
			return BadRequest();
		}

		return await HandleRequestAsync<AddPartRequest, ApiResponse<PartDTO>>(request, cancellationToken);
	}


	[HttpPut("parts/{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UpdatePartAsync(
		[FromRoute] int id,
		[FromBody] UpdatePartRequest request,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpDelete("parts/{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeletePartAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(
			new DeletePartRequest(id), 
			cancellationToken);
	}


	#endregion

	#region Branches

	[HttpGet("parts/{id:int}/branches")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartWithBranchesAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync<GetPartWithBranchesRequest, ApiResponse<ProjectPartDTO>>(
			new GetPartWithBranchesRequest(id), 
			cancellationToken);
	}


	[HttpGet("parts/branches/{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartBranchAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync<GetPartBranchRequest, ApiResponse<PartBranchDTO>>(
			new GetPartBranchRequest(id), 
			cancellationToken);
	}


	[HttpPost("parts/{id:int}/branches/{branchId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddBranchToPartAsync(
		[FromRoute] int id,
		[FromRoute] int branchId,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(
			new AddBranchToPartRequest(id, branchId), 
			cancellationToken);
	}


	[HttpDelete("parts/branches/{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> RemoveBranchFromPartAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(
			new RemoveBranchFromPartRequest(id), 
			cancellationToken);
	}

	#endregion

	#region Roles

	[HttpPost("parts/branches/roles")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddRoleToPartBranchAsync(
		[FromBody] AddRoleToPartBranchRequest request,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpDelete("parts/branches/roles/{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteRoleFromPartBranchAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync(
			new DeleteRoleFromPartBranchRequest(id), 
			cancellationToken);
	}

	#endregion
}
