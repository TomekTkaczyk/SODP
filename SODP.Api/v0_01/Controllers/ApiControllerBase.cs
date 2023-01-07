using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Model;
using SODP.Shared.DTO;
using System;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ApiControllerBase<T> : ControllerBase where T: BaseDTO 
    {
        protected readonly IEntityService<T> _service;
        protected readonly ILogger<ApiControllerBase<T>> _logger;

        public ApiControllerBase(IEntityService<T> service, ILogger<ApiControllerBase<T>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
