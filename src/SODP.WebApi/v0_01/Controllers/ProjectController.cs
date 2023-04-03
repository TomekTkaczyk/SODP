using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Projects;
using SODP.Application.Queries.Projects;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Domain.Shared;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace SODP.WebApi.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/projects")]
public class ProjectController : ApiControllerBase
{
	private readonly IProjectService _service;

	public ProjectController(
		IProjectService service,
		ISender sender,
		IMapper mapper,
		ILogger<ProjectController> logger)
		: base(sender, mapper, logger)
	{
		_service = service ?? throw new ArgumentNullException(nameof(service));
	}


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
			var error = Result.Failure(new Error("Project.BadRequest", "pageNumber and/or pageSize is invalid."));
			return BadRequest(error.Error);
		}

		var query = new GetProjectsPageQuery(status, searchString, pageNumber, pageSize);
		try
		{
			var response = await _sender.Send(query, cancellationToken);

			return response.IsSuccess
				? Ok(response)
				: NotFound(response.Errors);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAsync(
		int id,
		CancellationToken cancellationToken = default)
	{
		var query = new GetProjectByIdQuery(id);
		try
		{
			var response = await _sender.Send(query, cancellationToken);

			return response.IsSuccess
				? Ok(response)
				: NotFound(response.Errors);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}



	[HttpGet("{id}/details")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetWithDetailsAsync(
		int id,
		CancellationToken cancellationToken)
	{
		var query = new GetProjectByIdWithDetailsQuery(id);
		try
		{
			var response = await _sender.Send(query, cancellationToken);

			return response.IsSuccess
				? Ok(response)
				: NotFound(response.Errors);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateProjectCommand command,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await _sender.Send(command, cancellationToken);

			if (result.IsFailure)
			{
				switch (result.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return NotFound(result.Errors);
					case HttpStatusCode.Conflict:
						return Conflict(result.Errors);
				}
			}

			return CreatedAtAction(
				nameof(GetAsync),
				new { result.Value.Id },
				_mapper.Map<ProjectDTO>(result.Value));
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> DeleteAsync(
		int id,
		CancellationToken cancellationToken = default)
	{
		var command = new DeleteProjectCommand(id);
		try
		{
			var result = await _sender.Send(command, cancellationToken);

			return result.IsSuccess
				? NoContent()
				: NotFound(result.Errors);
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}


	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] UpdateProjectCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}

		try
		{
			var result = await _sender.Send(command, cancellationToken);

			if (result.IsFailure)
			{
				switch(result.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return NotFound(result.Errors);
					case HttpStatusCode.Conflict:
						return Conflict(result.Errors);
				}
			}
			return NoContent();
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}


		//if (id != project.Id)
		//{
		//	return BadRequest();
		//}

		//var response = await _service.UpdateAsync(project);
		//if (!response.Success)
		//{
		//	return StatusCode(response.StatusCode, response);
		//}

		//return NoContent();
	}


	[HttpPost("{id}/parts")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> AddPartAsync(int id, PartDTO part)
	{
		return Ok(await _service.AddPartAsync(id, part));
	}


	[HttpPut("parts/{partId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UpdatePartAsync(int partId, PartDTO part)
	{
		return Ok(await _service.UpdatePartAsync(partId, part));
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
