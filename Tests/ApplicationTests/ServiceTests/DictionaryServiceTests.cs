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
        public async Task when_call_GetPageAsync_with_specified_active_without_master_should_return_some_master_elements()
        {
            CreateFakeDictionaryData();
            ServicePageResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetPageAsync(null);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 6);

            response = await dictionaryService.GetPageAsync(true);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 4);

            response = await dictionaryService.GetPageAsync(false);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 2);
        }

        [Fact]
        public async Task when_call_GetPageAsync_with_specified_active_with_specified_empty_or_null_master_should_return_some_master_elements()
        {
            CreateFakeDictionaryData();
            ServicePageResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetPageAsync("", null);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 6);

            response = await dictionaryService.GetPageAsync("", true);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 4);

            response = await dictionaryService.GetPageAsync("", false);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 2);

            response = await dictionaryService.GetPageAsync(null, null);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 6);

            response = await dictionaryService.GetPageAsync(null, true);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 4);

            response = await dictionaryService.GetPageAsync(null, false);

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 2);
        }

        [Fact]
        public async Task when_call_GetPageAsync_with_specified_active_with_specified_master_should_return_some_slave_elements()
        {
            CreateFakeDictionaryData();
            ServicePageResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetPageAsync("EXIST", null);

            Assert.NotNull(response);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 4);

            response = await dictionaryService.GetPageAsync("EXIST", true);

            Assert.NotNull(response);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 3);

            response = await dictionaryService.GetPageAsync("EXIST", false);

            Assert.NotNull(response);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 1);
         }

        [Fact]
        public async Task when_call_GetPageAsync_with_specyfied_active_without_master_with_search_should_return_some_master_elements()
        {
            CreateFakeDictionaryData();
            ServicePageResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetPageAsync(null, searchString: "OTH");

            Assert.NotNull(response);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 4);

            response = await dictionaryService.GetPageAsync(true, searchString: "OTH");

            Assert.NotNull(response);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 2);

            response = await dictionaryService.GetPageAsync(false, searchString: "OTH");

            Assert.NotNull(response);
            Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 2);
        }

        [Fact]
        public async Task when_call_GetPageAsync_with_specyfied_active_with_master_with_search_should_return_some_master_elements()
        {
            CreateFakeDictionaryData();
            ServicePageResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetPageAsync("EXIST", null, searchString: "OTH");

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 3);

            response = await dictionaryService.GetPageAsync("EXIST", true, searchString: "OTH");

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 2);

            response = await dictionaryService.GetPageAsync("EXIST", false, searchString: "OTH");

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 1);
        }

        [Fact]
        public async Task when_call_GetAsync_should_return_not_found()
        {
            CreateFakeDictionaryData();
            ServiceResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetAsync("NOTEXIST");

            Assert.Equal(401, response.StatusCode);

            response = await dictionaryService.GetAsync("NOTEXIST", "EXIST1");

            Assert.Equal(401, response.StatusCode);

            response = await dictionaryService.GetAsync("EXIST", "NOTEXIST");

            Assert.Equal(401, response.StatusCode);
        }

        [Fact]
        public async Task when_call_GetAsync_with_specified_only_masterSign_should_return_one_master_element()
        {
            CreateFakeDictionaryData();
            ServiceResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetAsync("EXIST");

            Assert.True(response.Data.Sign.Equals("EXIST") && response.Data.Master.Equals(""));
        }

        [Fact]
        public async Task when_call_GetAsync_with_specified_masterSign_and_slaveSign_should_return_one_slave_element()
        {
            CreateFakeDictionaryData();
            ServiceResponse<DictionaryDTO> response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.GetAsync("EXIST","EXIST1");

            Assert.True(response.Data.Sign.Equals("EXIST1") && response.Data.Master.Equals("EXIST"));
        }

        [Fact]
        public async Task when_call_DeleteAsync_with_specified_not_exist_masterSign_should_return_not_found()
        {
            CreateFakeDictionaryData();
            ServiceResponse response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.DeleteAsync("NOTEXIST");

            Assert.Equal(401, response.StatusCode);

            response = await dictionaryService.DeleteAsync("NOTEXIST", "EXIST1");

            Assert.Equal(401, response.StatusCode);

            response = await dictionaryService.DeleteAsync("NOTEXIST", "NOTEXIST");

            Assert.Equal(401, response.StatusCode);
        }

        [Fact]
        public async Task when_call_DeleteAsync_with_specified_exist_masterSign_and_not_exist_slaveSign_should_return_not_found()
        {
            CreateFakeDictionaryData();
            ServiceResponse response;
            var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await dictionaryService.DeleteAsync("EXIST", "NOTEXIST");

            Assert.Equal(401, response.StatusCode);
        }
    }
}
