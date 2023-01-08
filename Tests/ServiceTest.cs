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
using System.Linq;

namespace Tests
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

        protected void CreateFakeDictionaryData()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.AppDictionary.AddRange(
                new AppDictionary { Id = 1, Sign = "OTHER1", Name = "INNY SŁOWNIK GŁÓWNY 1", ActiveStatus = true },
                new AppDictionary { Id = 2, Sign = "OTHER2", Name = "INNY SŁOWNIK GŁÓWNY 2", ActiveStatus = false },
                new AppDictionary { Id = 3, Sign = "PARTS", Name = "CZĘŚCI PROJEKTU", ActiveStatus = true },

                new AppDictionary { Id = 4, Master = "PARTS", Sign = "PZT", Name = "PROJEKT ZAGOSPODAROWANIA TERENU", ActiveStatus = true },
                new AppDictionary { Id = 5, Master = "PARTS", Sign = "PAB", Name = "PROJEKT ARCHITEKTONICZNO-BUDOWLANY", ActiveStatus = false },
                new AppDictionary { Id = 6, Master = "PARTS", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 10, Master = "PARTS", Sign = "PW", Name = "PROJEKT WYKONAWCZY", ActiveStatus = true },
                new AppDictionary { Id = 11, Master = "PARTS", Sign = "PW", Name = "PROJEKT WYKONAWCZY", ActiveStatus = true },
                new AppDictionary { Id = 12, Master = "PARTS", Sign = "PW", Name = "PROJEKT WYKONAWCZY", ActiveStatus = false },
                new AppDictionary { Id = 13, Master = "PARTS", Sign = "PW", Name = "PROJEKT WYKONAWCZY", ActiveStatus = true },

                new AppDictionary { Id = 7, Master = "OTHER1", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 8, Master = "OTHER2", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 9, Master = "OTHER3", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true }
                );

            _context.SaveChanges();

            _context.AppDictionary.First(x => x.Id == 2).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 5).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 12).ActiveStatus = false;
            _context.SaveChanges();
        }


    }
}
