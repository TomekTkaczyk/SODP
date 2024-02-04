using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SODP.Domain.Exceptions;
using SODP.Shared.Response;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SODP.WebApi.v0_01.Controllers;

public abstract class ApiBaseController : ControllerBase
{
	protected readonly ISender _sender;
	protected readonly ILogger<ApiBaseController> _logger;

	public ApiBaseController(ISender sender, ILogger<ApiBaseController> logger)
	{
		_sender = sender ?? throw new ArgumentNullException(nameof(sender));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}


	protected async Task<IActionResult> HandleRequestAsync<TRequest>(
		TRequest request, 
		CancellationToken cancellationToken) where TRequest : IRequest
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState
				.Where(x => x.Value.Errors.Any())
				.Select(x => new { property = x.Key, errors = x.Value.Errors }));
		}

        await _sender.Send(request, cancellationToken);

        return NoContent();
	}

	protected async Task<IActionResult> HandleRequestAsync<TRequest, TResponse>(
		TRequest request, 
		CancellationToken cancellationToken) where TRequest : IRequest<TResponse> where TResponse : ApiResponse
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState
				.Where(x => x.Value.Errors.Any())
				.Select(x => new { property = x.Key, errors = x.Value.Errors}));
		}

        var response = await _sender.Send(request, cancellationToken);

        return StatusCode((int)response.HttpCode, response);
	}
}
