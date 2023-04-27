using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Domain.Exceptions;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
	protected readonly ISender _sender;
	protected readonly ILogger<ApiControllerBase> _logger;
	protected readonly IMapper _mapper;

	public ApiControllerBase(ISender sender, ILogger<ApiControllerBase> logger, IMapper mapper)
	{
		_sender = sender ?? throw new ArgumentNullException(nameof(sender));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(logger));
	}


	protected async Task<IActionResult> HandleRequestAsync<TRequest>(
		TRequest request, 
		CancellationToken cancellationToken)
	where TRequest : IRequest
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState
				.Where(x => x.Value.Errors.Any())
				.Select(x => new { property = x.Key, errors = x.Value.Errors }));
		}
		try
		{
			await _sender.Send(request, cancellationToken);
			return NoContent();
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex);
		}
		catch (ConflictException ex)
		{
			return Conflict(ex);
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}

	protected async Task<IActionResult> HandleRequestAsync<TRequest, TResponse>(
		TRequest request, 
		CancellationToken cancellationToken)
		where TRequest : IRequest<TResponse>
		where TResponse : ApiResponse
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState
				.Where(x => x.Value.Errors.Any())
				.Select(x => new { property = x.Key, errors = x.Value.Errors}));
		}

		try
		{
			var response = await _sender.Send(request, cancellationToken);

			return StatusCode((int)response.HttpCode, response);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex);
		}
		catch (ConflictException ex)
		{
			return Conflict(ex);
		}
		catch (Exception ex)
		{
			return UnknowServerError(ex);
		}
	}

	protected ObjectResult UnknowServerError(object value)
	{
		return StatusCode(StatusCodes.Status500InternalServerError, value);
	}
}
