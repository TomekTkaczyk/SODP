using SODP.Domain.Entities;
using SODP.Domain.Exceptions.PartExceptions;
using Xunit;

namespace tests.ValidationTests;

public class ValidationBranchTests
{
	[Fact]
	internal void when_sign_is_null_then_throw_exception()
	{
		Assert.Throws<BranchSignIsInvalidException>(() => Branch.Create(null, ""));
	}
}
