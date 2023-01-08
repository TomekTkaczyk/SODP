using SODP.Application.Validators;
using SODP.Infrastructure.Services;
using SODP.Model;
using SODP.Shared.DTO;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ServiceTests
{
    public class BranchServiceTests : ServiceTest<Branch>, IDisposable
    {
        public BranchServiceTests() : base(new BranchValidator()) { }

        //[Fact]
        //public async Task CreateAsync_should_return_new_branch()
        //{
        //    _context.Database.EnsureDeleted();
        //    _context.Database.EnsureCreated();
        //    _context.SaveChanges();

        //    var service = new BranchService(_mapper, _validator, _context, _activeStatusServiceMock.Object);
        //    var result = await service.CreateAsync(new BranchDTO() { Sign = "A", Name = "ARCHITEKTURA"});
            
        //    Assert.NotNull(result);
        //    Assert.True(result.Data.Sign.Equals("A") && result.Data.Name.Equals("ARCHITEKTURA"));
        //}
    }
}
