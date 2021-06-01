﻿using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var req = HttpContext.Request.Query;
            int.TryParse(req["page_number"], out int page_number);
            int.TryParse(req["page_size"], out int page_size);
            return Ok(await _projectsService.GetAllAsync(currentPage: page_number, pageSize: page_size));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int Id)
        {
            return Ok(await _projectsService.GetAsync(Id));
        }
    }
}
