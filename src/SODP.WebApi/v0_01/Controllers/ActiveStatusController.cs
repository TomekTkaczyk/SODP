using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.API.Requests.Common;
using SODP.Domain.Entities;

namespace SODP.WebApi.v0_01.Controllers;

public abstract class ActiveStatusController<TEntity> : ApiControllerBase where TEntity : IActiveStatus, IBaseEntity
{
	public ActiveStatusController( 
		ISender sender, 
		ILogger<ActiveStatusController<TEntity>> logger,
		IMapper mapper)	: base(sender, logger, mapper) { }


	[HttpPatch("{id:int}/status/{status}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> SetActiveStatusAsync(
		int id,
		int status,
		CancellationToken cancellationToken = default)
	{
		var request = new SetActiveStatusRequest<TEntity>(id, status == 1);

		return await HandleRequestAsync<SetActiveStatusRequest<TEntity>>(request, cancellationToken);
	}
}
