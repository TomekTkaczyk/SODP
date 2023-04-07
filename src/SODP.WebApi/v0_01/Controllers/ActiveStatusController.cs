using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Common;
using SODP.Application.Commands.Investors;
using SODP.Application.Services;
using SODP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.WebApi.v0_01.Controllers;
																			  
public abstract class ActiveStatusController<TEntity> : ApiControllerBase where TEntity : IActiveStatus
{
	public ActiveStatusController(
		ISender sender,
		IMapper mapper,
		ILogger<ApiControllerBase> logger)
		: base(sender, mapper, logger)
	{
	}


	[HttpPatch("{id}/status/{status}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetActiveStatusAsync(
		int id,
		int status,
		CancellationToken cancellationToke = default)
	{
		var command = new SetActiveStatusCommand<TEntity>(id, status == 1);
		try
		{
			var result = await _sender.Send(command, cancellationToke);
			return NoContent();
		}
		catch (Exception ex)
		{
			return InternalServerErrorStatusCode(ex);
		}
	}
}
