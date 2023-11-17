using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPageAsync(
		ProjectStatus status,
		string searchString = "",
		int pageNumber = 1,
		int pageSize = 0,
		CancellationToken cancellationToken = default)
	{
		if (pageSize == 0 && pageNumber != 1)
		{
			return BadRequest($"pageNumber and/or pageSize is invalid.");
		}

		var request = new GetProjectsPageRequest(status, searchString, pageNumber, pageSize);

		var a = await HandleRequestAsync<GetProjectsPageRequest, ApiResponse<Page<ProjectDTO>>>(request, cancellationToken);

		return a;
	}

	#region Project

	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		int id,
		CancellationToken cancellationToken = default)
	{
		var request = new GetProjectRequest(id);

		return await HandleRequestAsync<GetProjectRequest, ApiResponse<ProjectDTO>>(request, cancellationToken);
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
				new { id = response.Value.Id },
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
		int id,
		CancellationToken cancellationToken = default)
	{
		var request = new DeleteProjectRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] UpdateProjectRequest request,
		CancellationToken cancellationToken)
	{
		if (id != request.Id)
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
		int id,
		CancellationToken cancellationToken)
	{
		var request = new ArchiveProjectRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpPatch("{id:int}/restore")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Restore(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new RestoreProjectRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpPatch("{id:int}/investor")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetInvestorAsync(
		int id,
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
	int id,
	CancellationToken cancellationToken)
	{
		var request = new GetProjectWithDetailsRequest(id);

		return await HandleRequestAsync<GetProjectWithDetailsRequest, ApiResponse<ProjectDTO>>(request, cancellationToken);
	}


	[HttpGet("parts/{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetProjectPartRequest(id);

		return await HandleRequestAsync<GetProjectPartRequest, ApiResponse<ProjectPartDTO>>(request, cancellationToken);
		//return Ok(await _service.GetProjectPartAsync(projectPartId));
	}


	[HttpPost("{id:int}/parts")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddPartAsync(
		int id,
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
		int id,
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
		int id,
		CancellationToken cancellationToken)
	{
		var request = new DeletePartRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
		//return Ok(await _service.DeleteProjectPartAsync(projectPartId));
	}


	#endregion

	#region Branches

	[HttpGet("parts/{id:int}/branches")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartWithBranchesAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetPartWithBranchesRequest(id);

		return await HandleRequestAsync<GetPartWithBranchesRequest, ApiResponse<ProjectPartDTO>>(request, cancellationToken);
		//return Ok(await _service.GetProjectPartWithBranchesAsync(projectPartId));
	}


	[HttpGet("parts/branches/{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartBranchAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetPartBranchRequest(id);

		return await HandleRequestAsync<GetPartBranchRequest, ApiResponse<PartBranchDTO>>(request, cancellationToken);
		//return Ok(await _service.GetPartBranchAsync(partBranchId));
	}


	[HttpPost("parts/{id:int}/branches/{branchId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddBranchToPartAsync(
		int id,
		int branchId,
		CancellationToken cancellationToken)
	{
		var request = new AddBranchToPartRequest(id, branchId);

		return await HandleRequestAsync(request, cancellationToken);
	}


	[HttpDelete("parts/branches/{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> RemoveBranchFromPartAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new RemoveBranchFromPartRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
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
		int id,
		CancellationToken cancellationToken)
	{
		var request = new DeleteRoleFromPartBranchRequest(id);

		return await HandleRequestAsync(request, cancellationToken);
	}

	#endregion
}
