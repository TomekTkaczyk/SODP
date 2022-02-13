using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using SODP.Api.v0_01.Controllers;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApiControllerTests
{
    public class DesignerControllerTests
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var service = Substitute.For<IDesignerService>();
            var logger = Substitute.For<ILogger<DesignerController>>();
            var controller = new DesignerController(service, logger);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
