using SODP.Application.API.Requests.Branches;
using Xunit;

namespace tests.ApplicationTests.ApiRequests;

public class RequestTests
{
	[Fact]
	internal void when_create_create_branch_request_record_then_stage_singn_is_capitalized()
	{
		var record = new CreateBranchRequest("tb", "Title branch");

		Assert.Equal("TB", record.Sign);
	}
}
