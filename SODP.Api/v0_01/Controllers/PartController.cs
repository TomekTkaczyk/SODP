using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
	[ApiController]
	[Route("api/v0_01/parts")]


	public class PartController : ApiControllerBase
	{
		private readonly IPartService _service;

		public PartController(IPartService service, ILogger<PartController> logger) : base(logger)
		{
			_service = service;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0, string searchString = "")
		{
			_logger.LogInformation("Get PartsPage...");
			return Ok(await _service.GetPageAsync(active, currentPage, pageSize, searchString));
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetAsync(int id)
		{
			return Ok(await _service.GetAsync(id));
		}



		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> CreateAsync([FromBody] PartDTO entity)
		{
			return Ok(await _service.CreateAsync(entity));
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public virtual async Task<IActionResult> UpdateAsync(int id, [FromBody] PartDTO entity)
		{
			if (id != entity.Id)
			{
				return BadRequest();
			}

			return Ok(await _service.UpdateAsync(entity));
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			return Ok(await _service.DeleteAsync(id));
		}


		[HttpPatch("{id}/status")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> SetActiveStatusAsync(int id, [FromBody] int status)
		{
			if (_service is IActiveStatusService)
			{
				return Ok(await (_service as IActiveStatusService).SetActiveStatusAsync(id, status == 1));
			}

			return BadRequest();
		}


	}
}
