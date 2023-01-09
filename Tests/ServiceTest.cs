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
                new AppDictionary { Id = 1, Sign = "EXIST", Name = "EXISTING MASTER ITEM", ActiveStatus = true },
                new AppDictionary { Id = 2, Sign = "EXIST1", Name = "ANOTHER MASTER ITEM", ActiveStatus = true },
                new AppDictionary { Id = 3, Sign = "EXIST2", Name = "ANOTHER MASTER ITEM", ActiveStatus = false },
                new AppDictionary { Id = 4, Sign = "EXIST3", Name = "EMPTY MASTER ITEM", ActiveStatus = true },
                new AppDictionary { Id = 5, Sign = "EXIST4", Name = "EMPTY ANOTHER MASTER ITEM", ActiveStatus = true },
                new AppDictionary { Id = 6, Sign = "EXIST5", Name = "EMPTY ANOTHER MASTER ITEM", ActiveStatus = false },

                new AppDictionary { Id = 7, Master = "EXIST", Sign = "EXIST1", Name = "EXIST SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 8, Master = "EXIST", Sign = "EXIST2", Name = "ANOTHER EXIST SLAVE ITEM", ActiveStatus = false },
                new AppDictionary { Id = 9, Master = "EXIST", Sign = "EXIST3", Name = "ANOTHER EXIST SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 10, Master = "EXIST", Sign = "EXIST4", Name = "ANOTHER EXIST SLAVE ITEM", ActiveStatus = true },

                new AppDictionary { Id = 11, Master = "EXIST1", Sign = "EXIST1", Name = "EXIST1 SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 12, Master = "EXIST1", Sign = "EXIST2", Name = "ANOTHER EXIST1 SLAVE ITEM", ActiveStatus = false },
                new AppDictionary { Id = 13, Master = "EXIST1", Sign = "EXIST3", Name = "ANOTHER EXIST1 SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 14, Master = "EXIST1", Sign = "EXIST4", Name = "ANOTHER EXIST1 SLAVE ITEM", ActiveStatus = false },

                new AppDictionary { Id = 15, Master = "EXIST2", Sign = "EXIST1", Name = "EXIST2 SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 16, Master = "EXIST2", Sign = "EXIST2", Name = "ANOTHER EXIST2 SLAVE ITEM", ActiveStatus = false },
                new AppDictionary { Id = 17, Master = "EXIST2", Sign = "EXIST3", Name = "ANOTHER EXIST2 SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 18, Master = "EXIST2", Sign = "EXIST4", Name = "ANOTHER EXIST2 SLAVE ITEM", ActiveStatus = true },

                new AppDictionary { Id = 19, Master = "ORPHANED", Sign = "EXIST1", Name = "ORPHANED SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 20, Master = "ORPHANED", Sign = "EXIST2", Name = "ORPHANED ANOTHER SLAVE ITEM", ActiveStatus = true },
                new AppDictionary { Id = 21, Master = "ORPHANED", Sign = "EXIST3", Name = "ORPHANED ANOTHER SLAVE ITEM", ActiveStatus = false }
                );

            _context.SaveChanges();

            _context.AppDictionary.First(x => x.Id == 3).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 6).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 8).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 12).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 14).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 16).ActiveStatus = false;
            _context.AppDictionary.First(x => x.Id == 21).ActiveStatus = false;
            _context.SaveChanges();
        }


    }
}
