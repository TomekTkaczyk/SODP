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
    [Route("/api/v0_01/dictionary")]
    public class DictionaryController : ApiControllerBase<DictionaryDTO>
    {
		public DictionaryController(IDictionaryService service, ILogger<DictionaryController> logger) : base(service, logger) { }
	}
}
