using SODP.Application.Validators;
using SODP.Infrastructure.Services;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ServiceTests
{
    public class BranchServiceTests : ServiceTest<Branch>, IDisposable
    {
        public BranchServiceTests() : base(new BranchValidator()) { }

        [Fact]
        public async Task when_call_GetPageAsync_should_return_all_elements()
        {
            CreateFakeBranchData();
            ServicePageResponse<BranchDTO> response;
            var branchService = new BranchService(_mapper, _validator, _context, _activeStatusServiceMock.Object);

            response = await branchService.GetPageAsync();

            Assert.True(response.Success);
            Assert.True(response.Data.Collection.Count == 4);
        }
    }
}
