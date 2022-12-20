using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/stages")]
    public class StageController : ApiControllerBase<StageDTO>
    {
        public StageController(IStageService service, ILogger<StageController> logger) : base(service, logger) { }
    }
}
