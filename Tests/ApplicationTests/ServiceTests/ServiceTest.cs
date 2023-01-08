using AutoMapper;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ApplicationTests.ServiceTests
{
    public abstract class ServiceTest<T> : IDisposable where T : BaseEntity
    {
        protected readonly SODPDBContext _context;
        protected readonly SqliteConnection _connection;
        protected readonly IValidator<T> _validator;
        protected readonly Mock<IActiveStatusService<T>> _activeStatusServiceMock;
        protected readonly IMapper _mapper;

        public ServiceTest(IValidator<T> validator)
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

            _validator = validator;

            _activeStatusServiceMock = new Mock<IActiveStatusService<T>>();
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Close();
        }
    }
}
