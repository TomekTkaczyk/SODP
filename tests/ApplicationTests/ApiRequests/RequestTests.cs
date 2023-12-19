using SODP.Application.API.Requests.Branches;
using Xunit;

namespace tests.ApplicationTests.ApiRequests;

public class RequestTests
{
	[Fact]
	internal void when_create_create_branch_request_record_then_stage_singn_is_capitalized()
	{
		var record = new CreateBranchRequest()
		{
			Sign = "tb",
			Title = "Title branch"
		}; 

		Assert.Equal("TB", record.Sign);
		Assert.Equal("TITLE BRANCH", record.Title);
	}
}
