using FluentValidation.TestHelper;
using SODP.Application.Validators;
using SODP.Domain.Entities;
using Xunit;

namespace Tests.ValidationTests;

public class ValidationBranchTest
{
	private BranchValidator _validator;

	public ValidationBranchTest()
	{
		_validator = new BranchValidator();
	}

	[Fact]
	public void should_have_error_when_sign_is_null()
	{
		var branch = Branch.Create(null, "");

		var result = _validator.TestValidate(branch);

		result.ShouldHaveValidationErrorFor(b => b.Sign);
	}

	[Fact]
	public void should_not_have_error_when_sign_is_specified()
	{
		var branch = Branch.Create("A", "");

		var result = _validator.TestValidate(branch);

		result.ShouldNotHaveValidationErrorFor(b => b.Sign);
	}
}