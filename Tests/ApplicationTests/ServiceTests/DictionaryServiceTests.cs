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
    //public class DictionaryServiceTests : ServiceTest<AppDictionary>, IDisposable
    //{
    //    public DictionaryServiceTests() : base(new DictionaryValidator()) { }


    //    [Fact]
    //    public async void when_created_new_item_default_value_ActiveStatus_is_true()
    //    {
    //        _context.Database.EnsureDeleted();
    //        _context.Database.EnsureCreated();

    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        var item = new DictionaryDTO()
    //        {
    //            Sign = "SPECIFIED",
    //            Name = "SPECIFIED"
    //        };
    //        var result = await dictionaryService.CreateAsync(item);

    //        Assert.True(result.Data.ActiveStatus);
    //    }

    //    [Fact]
    //    public async void when_created_new_item_specified_value_ActiveStatus_is_false()
    //    {
    //        _context.Database.EnsureDeleted();
    //        _context.Database.EnsureCreated();

    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        var item = new DictionaryDTO()
    //        {
    //            Sign = "SPECIFIED",
    //            Name = "SPECIFIED",
    //            ActiveStatus = false
    //        };
    //        var result = await dictionaryService.CreateAsync(item);

    //        Assert.False(result.Data.ActiveStatus);
    //    }

    //    [Fact]
    //    public async Task when_call_GetPageAsync_with_specified_active_without_master_should_return_some_master_elements()
    //    {
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);
    //        ServicePageResponse<DictionaryDTO> response;

    //        CreateFakeDictionaryData();
    //        response = await dictionaryService.GetPageAsync();

    //        Assert.True(response.Success);
    //        Assert.True(response.Data.Collection.Count == 6);

    //        //CreateFakeDictionaryData();
    //        //response = await dictionaryService.GetActive(true).GetPageAsync();

    //        CreateFakeDictionaryData();
    //        Assert.True(response.Success);
    //        Assert.True(response.Data.Collection.Count == 4);

    //        //CreateFakeDictionaryData();
    //        //response = await dictionaryService.GetActive(false).GetPageAsync();

    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 2);
    //    }

    //    [Fact]
    //    public async Task when_call_GetPageAsync_with_specified_active_with_specified_empty_or_null_master_should_return_some_master_elements()
    //    {
    //        CreateFakeDictionaryData();
    //        ServicePageResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.GetPageAsync();

    //        Assert.True(response.Success);
    //        Assert.True(response.Data.Collection.Count == 6);

    //        //response = await dictionaryService.GetActive(true).GetPageAsync();

    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 4);

    //        //response = await dictionaryService.GetActive(false).GetPageAsync();

    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 2);

    //        response = await dictionaryService.Parent("EXIST").GetPageAsync();

    //        Assert.True(response.Success);
    //        Assert.True(response.Data.Collection.Count == 4);

    //        response = await dictionaryService.Parent("EXIST").GetPageAsync();

    //        Assert.True(response.Success);
    //        Assert.True(response.Data.Collection.Count == 2);
    //    }

    //    [Fact]
    //    public async Task when_call_GetPageAsync_with_specified_active_with_specified_master_should_return_some_slave_elements()
    //    {
    //        CreateFakeDictionaryData();
    //        ServicePageResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.Parent("EXIST").GetPageAsync();

    //        Assert.NotNull(response);
    //        Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
    //        Assert.True(response.Success);
    //        Assert.True(response.Data.Collection.Count == 4);

    //        response = await dictionaryService.Parent("EXIST").GetPageAsync();

    //        Assert.NotNull(response);
    //        Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
    //        Assert.True(response.Success);
    //        Assert.True(response.Data.Collection.Count == 3);

    //        //response = await dictionaryService.Parent("EXIST").GetActive(false).GetPageAsync();

    //        //Assert.NotNull(response);
    //        //Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 1);
    //     }

    //    [Fact]
    //    public void when_call_GetPageAsync_with_specyfied_active_without_master_with_search_should_return_some_master_elements()
    //    {
    //        //CreateFakeDictionaryData();
    //        //ServicePageResponse<DictionaryDTO> response;
    //        //var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        //response = await dictionaryService.WithSearchString("OTH").GetPageAsync();

    //        //Assert.NotNull(response);
    //        //Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 4);

    //        //response = await dictionaryService.WithSearchString("OTH").GetActive(true).GetPageAsync();

    //        //Assert.NotNull(response);
    //        //Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 2);

    //        //response = await dictionaryService.WithSearchString("OTH").GetActive(false).GetPageAsync();

    //        //Assert.NotNull(response);
    //        //Assert.IsType<ServicePageResponse<DictionaryDTO>>(response);
    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 2);
    //    }

    //    [Fact]
    //    public void when_call_GetPageAsync_with_specyfied_active_with_master_with_search_should_return_some_master_elements()
    //    {
    //        //CreateFakeDictionaryData();
    //        //ServicePageResponse<DictionaryDTO> response;
    //        //var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        //response = await dictionaryService.Parent("EXIST").WithSearchString("OTH").GetPageAsync();

    //        //Assert.NotNull(response);
    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 3);

    //        //response = await dictionaryService.Parent("EXIST").GetActive(true).GetPageAsync();

    //        //Assert.NotNull(response);
    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 2);

    //        //response = await dictionaryService.Parent("EXIST").GetActive(false).SearchFilter("OTH").GetPageAsync();

    //        //Assert.NotNull(response);
    //        //Assert.True(response.Success);
    //        //Assert.True(response.Data.Collection.Count == 1);
    //    }

    //    [Fact]
    //    public async Task when_call_GetAsync_should_return_not_found()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.GetAsync("NOTEXIST");

    //        Assert.Equal(404, response.StatusCode);

    //        response = await dictionaryService.GetAsync("NOTEXIST", "EXIST1");

    //        Assert.Equal(404, response.StatusCode);

    //        response = await dictionaryService.GetAsync("EXIST", "NOTEXIST");

    //        Assert.Equal(404, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_GetMasterAsync_should_return_master_with_slaves()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.GetMasterAsync("EXIST");

    //        Assert.Equal("EXIST", response.Data.Sign);
    //        Assert.Equal(4, response.Data.Children.Count);
    //    }

    //    [Fact]
    //    public async Task when_call_GetAsync_with_specified_only_masterSign_should_return_one_master_element()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.GetAsync("EXIST");

    //        Assert.True(response.Data.Sign.Equals("EXIST") && response.Data.ParentId.Equals(1));
    //    }

    //    [Fact]
    //    public async Task when_call_GetAsync_with_specified_masterSign_and_slaveSign_should_return_one_slave_element()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.GetAsync("EXIST","EXIST1");

    //        Assert.True(response.Data.Sign.Equals("EXIST1") && response.Data.ParentId.Equals(1));
    //    }

    //    [Fact]
    //    public async Task when_call_GetAsync_with_id_should_return_one_element()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.GetAsync(3);

    //        Assert.True(response.Data.Sign.Equals("EXIST2") && response.Data.ParentId.Equals(0) && response.Data.Name.Equals("ANOTHER MASTER ITEM"));
            
    //        response = await dictionaryService.GetAsync(15);

    //        Assert.True(response.Data.Sign.Equals("EXIST1") && response.Data.ParentId.Equals(3) && response.Data.Name.Equals("EXIST2 SLAVE ITEM"));
    //    }

    //    [Fact]
    //    public async Task when_call_GetAsync_with_id_if_not_exist_should_return_not_found()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.GetAsync(22);

    //        Assert.Equal(404, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_DeleteAsync_master_with_slave_should_return_no_content()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.DeleteAsync("EXIST");
    //        var count = await _context.Set<AppDictionary>().CountAsync();

    //        Assert.Equal(16, count);
    //        Assert.Equal(204, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_DeleteAsync_master_without_slave_should_return_no_content()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.DeleteAsync("EXIST4");
    //        var count = await _context.Set<AppDictionary>().CountAsync();

    //        Assert.Equal(20, count);
    //        Assert.Equal(204, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_DeleteAsync_with_specified_exist_master_and_not_exist_slave_should_return_not_found()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.DeleteAsync("EXIST", "NOTEXIST");

    //        Assert.Equal(404, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_DeleteAsync_with_specified_not_exist_master_should_return_not_found()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        response = await dictionaryService.DeleteAsync("NOTEXIST");

    //        Assert.Equal(404, response.StatusCode);

    //        response = await dictionaryService.DeleteAsync("NOTEXIST", "EXIST1");

    //        Assert.Equal(404, response.StatusCode);

    //        response = await dictionaryService.DeleteAsync("NOTEXIST", "NOTEXIST");

    //        Assert.Equal(404, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_CreateAsync_should_return_new_entity()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        var item = new DictionaryDTO()
    //        {
    //            Sign = "NEWSIGN",
    //            Name = "NEWNAME"
    //        };

    //        response = await dictionaryService.CreateAsync(item);

    //        Assert.Equal(response.Data.Sign, item.Sign);
    //        Assert.Equal(response.Data.Name, item.Name);

    //        item = new DictionaryDTO()
    //        {
    //            ParentId = 1,
    //            Sign = "NEWSIGN",
    //            Name = "NEWNAME"
    //        };

    //        response = await dictionaryService.CreateAsync(item);

    //        Assert.Equal(response.Data.ParentId, item.ParentId);
    //        Assert.Equal(response.Data.Sign, item.Sign);
    //        Assert.Equal(response.Data.Name, item.Name);
    //    }

    //    [Fact]
    //    public async Task when_call_CreateAsync_and_Sign_exist_should_return_conflict()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse<DictionaryDTO> response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        var item = new DictionaryDTO()
    //        {
    //            Sign = "EXIST",
    //            Name = "SOME TEXT"
    //        };

    //        response = await dictionaryService.CreateAsync(item);

    //        Assert.Equal(409, response.StatusCode);

    //        item = new DictionaryDTO()
    //        {
    //            ParentId = 1,
    //            Sign = "EXIST2",
    //            Name = "SOME TEXT"
    //        };

    //        response = await dictionaryService.CreateAsync(item);

    //        Assert.Equal(409, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_UpdateAsync_should_return_no_content()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        var item = new DictionaryDTO()
    //        {
    //            Id = 1,
    //            Sign = "NEWSIGN",
    //            Name = "NEWNAME"
    //        };
    //        response = await dictionaryService.UpdateAsync(item);
    //        var entity = await _context.AppDictionary.FirstAsync(x => x.Id == item.Id);

    //        Assert.Equal(0, entity.ParentId);
    //        Assert.Equal("EXIST", entity.Sign);
    //        Assert.Equal("NEWNAME", entity.Name);
    //        Assert.Equal(204, response.StatusCode);

    //        item.Id = 13;
    //        response = await dictionaryService.UpdateAsync(item);
    //        entity = await _context.AppDictionary.FirstAsync(x => x.Id == item.Id);

    //        Assert.Equal("EXIST1", entity.Parent.Sign);
    //        Assert.Equal("EXIST3", entity.Sign);
    //        Assert.Equal("NEWNAME", entity.Name);
    //        Assert.Equal(204, response.StatusCode);
    //    }

    //    [Fact]
    //    public async Task when_call_UpdateAsync_and_id_not_exist_should_return_not_found()
    //    {
    //        CreateFakeDictionaryData();
    //        ServiceResponse response;
    //        var dictionaryService = new DictionaryService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

    //        var item = new DictionaryDTO()
    //        {
    //            Id = 22,
    //            Sign = "NEWSIGN",
    //            Name = "NEWNAME"
    //        };
    //        response = await dictionaryService.UpdateAsync(item);

    //        Assert.Equal(404, response.StatusCode);
    //    }
    //}
}
