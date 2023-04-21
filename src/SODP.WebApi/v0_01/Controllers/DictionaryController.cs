using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.WebApi.v0_01.Controllers
{
    [ApiController]
    [Route("/api/v0_01/dictionary")]
    public class DictionaryController : ApiControllerBase
    {
        private readonly IDictionaryService _service;
        public DictionaryController(
            ISender sender,
            IMapper mapper,
            ILogger<DictionaryController> logger)
            : base(sender, mapper, logger) { }
	}
}
