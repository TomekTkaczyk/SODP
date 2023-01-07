using AutoMapper;
using DocumentFormat.OpenXml.Vml;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NSubstitute;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Infrastructure.Services;
using SODP.Model;
using SODP.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ServiceTests
{
    public class BranchTests
    {
        [Fact]
        public async void SampleTest()
        {
            var context = new Mock<SODPDBContext>();
            var branchMock = new Mock<DbSet<Branch>>();
            
			context.Setup(x => x.Branches).Returns((Branch u) => branchMock);

            var mapper = new Mock<IMapper>();

            var validator = new Mock<IValidator<Branch>>();
            
            var activeStatusService = new Mock<IActiveStatusService<Branch>>();

            var branchService = new BranchService(mapper.Object, validator.Object, context.Object, activeStatusService.Object);

            var result = await branchService.GetAsync(1);

            Assert.NotNull(result);
        }
    }
}
