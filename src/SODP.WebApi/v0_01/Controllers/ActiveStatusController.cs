using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Investors;
using SODP.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.WebApi.v0_01.Controllers;

public class ActiveStatusController : ApiControllerBase
{
	public ActiveStatusController(
		ISender sender,
		IMapper mapper,
		ILogger<ApiControllerBase> logger)
		: base(sender, mapper, logger)
	{
	}


	[HttpPatch("{id}/status")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetActiveStatusAsync(
		int id,
		[FromBody] SetActiveStatusCommand command,
		CancellationToken cancellationToke = default)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}

		try
		{
			var result = await _sender.Send(command, cancellationToke);
			return result.IsSuccess
				? NoContent()
				: NotFound();
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}
}
