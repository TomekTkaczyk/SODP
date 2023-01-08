using Microsoft.EntityFrameworkCore;
using Moq;
using SODP.Application.Validators;
using SODP.DataAccess;
using SODP.Infrastructure.Services;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ServiceTests
{
    public class DictionaryServiceTests : ServiceTest<AppDictionary>, IDisposable
    {
        public DictionaryServiceTests() : base(new DictionaryValidator()) { }


        [Fact]
        public void when_add_AppDictionary_entity_to_sqlight_memory_database_ActiveStatus_should_by_false()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var dictionary = new AppDictionary()
            {
                Sign = "PARTS",
                Name = "CZĘŚCI PROJEKTU"
            };

            var entity = _context.AppDictionary.Add(dictionary);
            _context.SaveChanges();

            Assert.False(entity.Entity.ActiveStatus);

        }

        [Fact]
        public void CreateAsync_whent_created_new_item_default_value_ActiveStatus_is_true()
        {
            var item = new AppDictionary();

            Assert.True(item.ActiveStatus);
        }

        [Fact]
        public void all_methods_not_execute_exceptions()
        {

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.SaveChanges();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.CreateAsync(new DictionaryDTO())));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.DeleteAsync(1)));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.DeleteAsync("", "")));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.ExistAsync(1)));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.GetAsync(1)));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.GetAsync("", "")));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.GetPageAsync(true, 1, 1)));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.GetPageAsync(true, 1, 1, "")));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.GetPageAsync("", true, 1, 1, "")));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.SetActiveStatusAsync(1, true)));
            Assert.NotNull(Record.ExceptionAsync(() => dictionaryService.UpdateAsync(new DictionaryDTO())));
        }

        [Fact]
        public async Task GetPageAsync_with_active_without_master_and_search_should_return_some_master_elements()
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
                new AppDictionary { Id = 7, Master = "OTHER1", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 8, Master = "OTHER2", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 9, Master = "OTHER3", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 10, Master = "PARTS", Sign = "PW", Name = "PROJEKT WYKONAWCZY", ActiveStatus = true });

            _context.SaveChanges();

            _context.Database.ExecuteSqlCommand("UPDATE AppDictionary SET ActiveStatus=0");


            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);
            ServicePageResponse<DictionaryDTO> result;

            result = await dictionaryService.GetPageAsync(null, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 3);

            result = await dictionaryService.GetPageAsync(true, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 2);

            result = await dictionaryService.GetPageAsync(false, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 1);
        }

        [Fact]
        public async Task GetPageAsync_with_active_with_master_without_search_should_return_some_master_elements()
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
                new AppDictionary { Id = 7, Master = "OTHER1", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 8, Master = "OTHER2", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 9, Master = "OTHER3", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 10, Master = "PARTS", Sign = "PW", Name = "PROJEKT WYKONAWCZY", ActiveStatus = true });

            _context.SaveChanges();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);
            ServicePageResponse<DictionaryDTO> result;

            result = await dictionaryService.GetPageAsync("", null, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 3);

            result = await dictionaryService.GetPageAsync("", true, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 2);

            result = await dictionaryService.GetPageAsync("", false, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 1);

            result = await dictionaryService.GetPageAsync(null, null, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 3);

            result = await dictionaryService.GetPageAsync(null, true, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 2);

            result = await dictionaryService.GetPageAsync(null, false, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 1);


            result = await dictionaryService.GetPageAsync(null, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 3);

            result = await dictionaryService.GetPageAsync(true, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 2);

            result = await dictionaryService.GetPageAsync(false, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 1);
        }

        private Mock<SODPDBContext> CreateDbContext()
        {
            var dictionary = GetFakeData().AsQueryable();

            var dbSet = new Mock<DbSet<AppDictionary>>();
            dbSet.As<IQueryable<AppDictionary>>().Setup(m => m.Provider).Returns(dictionary.Provider);
            dbSet.As<IQueryable<AppDictionary>>().Setup(m => m.Expression).Returns(dictionary.Expression);
            dbSet.As<IQueryable<AppDictionary>>().Setup(m => m.ElementType).Returns(dictionary.ElementType);
            dbSet.As<IQueryable<AppDictionary>>().Setup(m => m.GetEnumerator()).Returns(dictionary.GetEnumerator());

            var dbContextMock = new Mock<SODPDBContext>();
            dbContextMock.Setup(c => c.AppDictionary).Returns(dbSet.Object);

            return dbContextMock;
        }

        private IEnumerable<AppDictionary> GetFakeData()
        {
            var dictionary = new List<AppDictionary>{
                new AppDictionary { Id = 1, Sign = "OTHER1", Name = "INNY SŁOWNIK GŁÓWNY 1", ActiveStatus = true },
                new AppDictionary { Id = 2, Sign = "OTHER2", Name = "INNY SŁOWNIK GŁÓWNY 2", ActiveStatus = false },
                new AppDictionary { Id = 3, Sign = "PARTS", Name = "CZĘŚCI PROJEKTU", ActiveStatus = true },
                new AppDictionary { Id = 4, Master = "PARTS", Sign = "PZT", Name = "PROJEKT ZAGOSPODAROWANIA TERENU", ActiveStatus = true },
                new AppDictionary { Id = 5, Master = "PARTS", Sign = "PAB", Name = "PROJEKT ARCHITEKTONICZNO-BUDOWLANY", ActiveStatus = false },
                new AppDictionary { Id = 6, Master = "PARTS", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 7, Master = "OTHER1", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 8, Master = "OTHER2", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 9, Master = "OTHER3", Sign = "PT", Name = "PROJEKT TECHNICZNY", ActiveStatus = true },
                new AppDictionary { Id = 10, Master = "PARTS", Sign = "PW", Name = "PROJEKT WYKONAWCZY", ActiveStatus = true }
            };

            return dictionary.Select(_ => _);
        }
    }
}
