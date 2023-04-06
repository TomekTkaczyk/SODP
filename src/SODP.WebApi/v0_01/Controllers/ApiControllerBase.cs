using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using System.Net;

namespace SODP.WebApi.v0_01.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
	protected readonly ISender _sender;
	protected readonly IMapper _mapper;
	protected readonly ILogger<ApiControllerBase> _logger;

	public ApiControllerBase(ISender sender, IMapper mapper, ILogger<ApiControllerBase> logger)
	{
		_sender = sender ?? throw new ArgumentNullException(nameof(sender));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	protected ObjectResult InternalServerErrorStatusCode(object value)
	{
		return StatusCode(StatusCodes.Status500InternalServerError, value);
	}

	protected IActionResult GetActionResult(ApiResponse response)
	{
		switch(response.StatusCode)
		{
			case HttpStatusCode.NoContent:
				return NoContent();
			case HttpStatusCode.NotFound:
				return NotFound(response.Errors);
			case HttpStatusCode.Conflict:
				return Conflict(response.Errors);
			default:
				throw new Exception($"{response.StatusCode} not implemented.");
		}
	}
}
