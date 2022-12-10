using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ILogger<ApiControllerBase> logger;

        public ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            this.logger = logger;
        }
    }
}
