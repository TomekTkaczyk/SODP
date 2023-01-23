using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ApiControllerBase : ControllerBase 
    {
        protected readonly ILogger<ApiControllerBase> _logger;

        public ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
