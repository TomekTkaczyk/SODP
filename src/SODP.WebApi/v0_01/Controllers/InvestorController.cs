using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Commands.Investors;
using SODP.Application.Services;
using SODP.Domain.Entities;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.WebApi.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/investors")]
    public class InvestorController : ApiControllerBase
	{
        private readonly ISender _sender;
        private readonly IInvestorService _service;
		private readonly IMapper _mapper;

		public InvestorController(ISender sender, IInvestorService service, ILogger<InvestorController> logger, IMapper mapper) 
            : base(logger)
        {
            _sender = sender;
            _service = service;
			_mapper = mapper;
		}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetPageAsync(bool? active, string searchString, int currentPage = 1, int pageSize = 0)
        {
            return Ok(await _service.GetPageAsync(active, searchString, currentPage, pageSize));
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
        [ProducesResponseType(typeof(Result<InvestorDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken, [FromBody] CreateInvestorCommand command)
        {
            // var command = new CreateInvestorCommand(investor.Name);

            var result = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetAsync), new { result.Data.Id }, _mapper.Map<Investor,InvestorDTO>(result.Data));
		}


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<IActionResult> UpdateAsync(int id, [FromBody] InvestorDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            await _service.UpdateAsync(entity);

			return NoContent();
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
