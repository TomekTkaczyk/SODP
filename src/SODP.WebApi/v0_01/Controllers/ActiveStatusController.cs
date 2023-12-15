using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Common;
using SODP.Domain.Entities;

namespace SODP.WebApi.v0_01.Controllers;

public abstract class ActiveStatusController<TEntity> : ApiBaseController where TEntity : IActiveStatus, IBaseEntity
{
	public ActiveStatusController(
		ISender sender,
		ILogger<ActiveStatusController<TEntity>> logger,
		IMapper mapper) : base(sender, logger, mapper) { }


	[HttpPatch("{id:int}/status/{status:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetActiveStatusAsync(
		[FromRoute] int id,
		[FromRoute] int status,
		CancellationToken cancellationToken = default)
	{
		if (status > 1 && status < 0)
		{
			return BadRequest();
		}

		var request = new SetActiveStatusRequest<TEntity>(id, status == 1);

		return await HandleRequestAsync<SetActiveStatusRequest<TEntity>>(request, cancellationToken);
	}
}
