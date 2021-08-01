using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("/api/v0_01/branches")]
    public class BranchController : ApiControllerBase
    {
        private readonly IBranchService _service;

        public BranchController(IBranchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<BranchDTO>> Create([FromBody] BranchDTO branch)
        {
            var response = await _service.CreateAsync(branch);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //[HttpPut("{sign}")]
        //public async Task<ActionResult<BranchDTO>> Update(string sign, [FromBody] BranchDTO branch)
        //{
        //    if (sign != branch.Sign)
        //    {
        //        return BadRequest();
        //    }
        //    var response = await _service.UpdateAsync(branch);
        //    if (!response.Success)
        //    {
        //        return BadRequest(response);
        //    }

        //    return Ok(response);
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<BranchDTO>> Update(int id, [FromBody] BranchDTO branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            return Ok(await _service.UpdateAsync(branch));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

        [HttpPut("{Id}/{status}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] int status)
        {
            return Ok(await _service.SetActiveStatusAsync(id, status == 1));
        }


        [HttpGet("{id}/designers")]
        public async Task<IActionResult> GetLicencesAsync(int id)
        {
            return Ok(await _service.GetLicencesAsync(id));
        }
    }
}
