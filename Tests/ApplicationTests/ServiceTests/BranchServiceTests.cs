using AutoMapper;
using Castle.Core.Logging;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Vml;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NSubstitute;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Infrastructure.Services;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Interfaces;
using SODP.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ServiceTests
{
    public class BranchServiceTests : IDisposable
    {
        private readonly SODPDBContext _context;
        private readonly SqliteConnection _connection;
        private readonly Mock<IActiveStatusService<Branch>> _activeStatusService = new();
        private readonly IMapper _mapper;

        public BranchServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SODP.Domain.AutoMapperProfile());
            });
            _mapper = new Mapper(mapperConfig);
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<SODPDBContext>().UseSqlite(_connection).Options;
            _context = new SODPDBContext(options, new DateTimeService());
            _context.Database.EnsureCreated();



        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Close();
        }

        [Fact]
        public async Task CreateAsync_return_new_branch()
        {
            var branch = _mapper.Map<Branch>(new BranchDTO());
            var _validatorMock = new Mock<IValidator<Branch>>();

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<Branch>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));

            var service = new BranchService(_mapper, _validatorMock.Object, _context, _activeStatusService.Object);
            var result = await service.CreateAsync(new BranchDTO() { Sign = "A", Name = "ARCHITEKTURA"});
            
            Assert.NotNull(result);
            Assert.True(result.Data.Sign.Equals("A") && result.Data.Name.Equals("ARCHITEKTURA"));
        }
    }
}
