using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/investors")]
    public class InvestorController : ApiControllerBase<InvestorDTO>
    {
        public InvestorController(IInvestorService service, ILogger<InvestorController> logger) : base(service, logger) { }

	}
}
