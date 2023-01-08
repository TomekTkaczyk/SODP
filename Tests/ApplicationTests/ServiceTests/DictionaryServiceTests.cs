using Microsoft.EntityFrameworkCore;
using SODP.Application.Validators;
using SODP.Infrastructure.Services;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ServiceTests
{
    public class DictionaryServiceTests : ServiceTest<AppDictionary>, IDisposable
    {
        public DictionaryServiceTests() : base(new DictionaryValidator()) { }


        [Fact]
        public async void when_created_new_item_default_value_ActiveStatus_is_true()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            var item = new DictionaryDTO()
            {
                Sign = "SPECIFIED",
                Name = "SPECIFIED"
            };
            var result = await dictionaryService.CreateAsync(item);

            Assert.True(result.Data.ActiveStatus);
        }

        [Fact]
        public async void when_created_new_item_specified_value_ActiveStatus_is_false()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            var item = new DictionaryDTO()
            {
                Sign = "SPECIFIED",
                Name = "SPECIFIED",
                ActiveStatus = false
            };
            var result = await dictionaryService.CreateAsync(item);

            Assert.False(result.Data.ActiveStatus);
        }

        [Fact]
        public void all_methods_do_not_throw_exception()
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
        public async Task when_call_GetPageAsync_with_specified_active_without_master_and_search_should_return_all_master_elements()
        {
            CreateFakeDictionaryData();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            var result = await dictionaryService.GetPageAsync(null, currentPage: 1, pageSize: 0);

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
        public async Task when_call_GetPageAsync_with_specified_active_with_empty_or_null_master_without_search_should_return_some_master_elements()
        {
            CreateFakeDictionaryData();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            var result = await dictionaryService.GetPageAsync("", null, currentPage: 1, pageSize: 0);

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
        }

        [Fact]
        public async Task when_call_GetPageAsync_with_specified_active_with_specified_master_without_search_should_return_some_slave_elements()
        {
            CreateFakeDictionaryData();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            var result = await dictionaryService.GetPageAsync("PARTS", null, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 7);

            result = await dictionaryService.GetPageAsync("PARTS", true, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 5);

            result = await dictionaryService.GetPageAsync("PARTS", false, currentPage: 1, pageSize: 0);

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 2);
         }

        [Fact]
        public async Task when_call_GetPageAsync_with_specyfied_active_without_master_with_search_should_return_some_master_elements()
        {
            CreateFakeDictionaryData();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            var result = await dictionaryService.GetPageAsync(null, currentPage: 1, pageSize: 0, searchString: "GŁÓW");

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 2);

            result = await dictionaryService.GetPageAsync(true, currentPage: 1, pageSize: 0, searchString: "GŁÓW");

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 1);

            result = await dictionaryService.GetPageAsync(false, currentPage: 1, pageSize: 0, searchString: "GŁÓW");

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 1);
        }

        [Fact]
        public async Task when_call_GetPageAsync_with_specyfied_active_with_master_with_search_should_return_some_master_elements()
        {
            CreateFakeDictionaryData();

            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            var result = await dictionaryService.GetPageAsync("PARTS", null, currentPage: 1, pageSize: 0, searchString: "KONA");

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 4);

            result = await dictionaryService.GetPageAsync("PARTS", true, currentPage: 1, pageSize: 0, searchString: "KONA");

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 3);

            result = await dictionaryService.GetPageAsync("PARTS", false, currentPage: 1, pageSize: 0, searchString: "KONA");

            Assert.NotNull(result);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Collection.Count == 1);
        }

    }
}
