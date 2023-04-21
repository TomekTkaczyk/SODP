using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Handlers.Projects;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Services;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/projects")]
public class ProjectController : ApiControllerBase
{
	private readonly IProjectService _service;

	public ProjectController(
		ISender sender,
		IMapper mapper,
		ILogger<ProjectController> logger)
		: base(sender, mapper, logger) { }


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

		return await HandleRequestAsync<GetProjectsPageRequest, ApiResponse<Page<ProjectDTO>>>(request, cancellationToken);
	}


	[HttpGet("{id}")]
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



	[HttpGet("{id}/details")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetWithDetailsAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var request = new GetProjectWithDetailsRequest(id);

		return await HandleRequestAsync<GetProjectWithDetailsRequest,ApiResponse<ProjectDTO>>(request, cancellationToken);
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
		return await HandleRequestAsync<CreateProjectRequest,ApiResponse<ProjectDTO>>(request, cancellationToken);
	}


	[HttpDelete("{id}")]
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


	[HttpPut("{id}")]
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


	[HttpPost("{id}/parts")]
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

		return await HandleRequestAsync<AddPartRequest,ApiResponse<PartDTO>>(request, cancellationToken);
	}


	[HttpPut("parts/{partId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UpdatePartAsync(
		int partId, 
		UpdatePartRequest request,
		CancellationToken cancellationToken)
	{
		return await HandleRequestAsync<UpdatePartRequest>(request, cancellationToken);
	}


	[HttpPost("parts/{partId}/branches/{branchId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddBranchToPartAsync(int partId, int branchId)
	{
		_logger.LogInformation($"Strzał do parts/{partId}/branches/{branchId}");
		return Ok(await _service.AddBranchToPartAsync(partId, branchId));
	}


	[HttpDelete("parts/branches/{branchId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteBranchFromPartAsync(int branchId)
	{
		return Ok(await _service.DeletePartBranchAsync(branchId));
	}


	[HttpPost("parts/branches/roles")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddRoleToPartBranchAsync([FromBody] NewPartBranchRoleDTO role)
	{
		return Ok(await _service.AddRoleToPartBranchAsync(role.partBranchId, role.Role, role.LicenseId));
	}


	[HttpDelete("parts/branches/roles/{branchRoleId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteRoleFromPartBranchAsync(int branchRoleId)
	{
		return Ok(await _service.DeleteBranchRoleAsync(branchRoleId));
	}


	[HttpGet("parts/{projectPartId}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartAsync(int projectPartId)
	{
		return Ok(await _service.GetProjectPartAsync(projectPartId));
	}


	[HttpGet("parts/{projectPartId}/branches")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartWithBranchesAsync(int projectPartId)
	{
		return Ok(await _service.GetProjectPartWithBranchesAsync(projectPartId));
	}

	[HttpDelete("parts/{projectPartId}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeletePartAsync(int projectPartId)
	{
		return Ok(await _service.DeleteProjectPartAsync(projectPartId));
	}


	[HttpGet("parts/branches/{partBranchId}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetPartBranchAsync(int partBranchId)
	{
		return Ok(await _service.GetPartBranchAsync(partBranchId));
	}


	[HttpPatch("{id}/archive")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Archive(int id)
	{
		return Ok(await ((IProjectService)_service).ArchiveAsync(id));
	}


	[HttpPatch("{id}/restore")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Restore(int id)
	{
		return Ok(await ((IProjectService)_service).RestoreAsync(id));
	}


	[HttpPatch("{id}/investor")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetInvestorAsync(int id, [FromBody] string investor)
	{
		return Ok(await ((IProjectService)_service).SetInvestorAsync(id, investor));
	}
}
