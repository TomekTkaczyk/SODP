using FluentValidation;
using SODP.Domain.Entities;

namespace SODP.Application.Validators;

internal class CreateLicenseRequestValidator : AbstractValidator<License>
{
	public CreateLicenseRequestValidator()
    {
		RuleFor(u => u.Content)
			.NotNull()
			.NotEmpty().When(u => u.Content != null, ApplyConditionTo.CurrentValidator)
			.WithMessage("Content is required.")
			.WithName("License content");

		RuleFor(u => u.Designer)
			.NotNull()
			.WithMessage("Designer is required.");
	}
}
