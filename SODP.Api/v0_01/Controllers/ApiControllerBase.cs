using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Model;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ApiControllerBase<T> : ControllerBase where T: BaseDTO 
    {
        protected readonly IEntityService<T> _service;
        protected readonly ILogger<ApiControllerBase<T>> _logger;

        public ApiControllerBase(IEntityService<T> service, ILogger<ApiControllerBase<T>> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<IActionResult> GetPageAsync(int currentPage = 1, int pageSize = 0)
        {
            return Ok(await _service.GetPageAsync(currentPage, pageSize));
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
        public async Task<IActionResult> CreateAsync([FromBody] T entity)
        {
            var response = await _service.CreateAsync(entity);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] T entity)
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
            var response = await _service.DeleteAsync(id);

            return Ok(response);
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
