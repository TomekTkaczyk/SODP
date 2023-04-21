using FluentValidation;
using SODP.Application.Services;
using SODP.Domain.Entities;

namespace SODP.Application.Validators
{
    //public class LicenseValidator : AbstractValidator<License>
    //{
    //    private readonly IDesignerService _designerService;
    //    public LicenseValidator(IDesignerService designerService)
    //    {
    //        _designerService = designerService;

    //        RuleFor(u => u.Content)
    //            .NotNull()
    //            .NotEmpty().When(u => u.Content != null, ApplyConditionTo.CurrentValidator)
    //            .WithMessage("Number is requered.")
    //            .WithName("Content");

    //        RuleFor(u => u.Designer)
    //            .NotNull()
    //            .WithMessage("Designer is required.")
    //            .MustAsync((designer, cancellation) => _designerService.ExistAsync(designer.Id))
    //            .When(u => u.Designer != null, ApplyConditionTo.CurrentValidator)
    //            .WithMessage(u => $"Designer:{u.Designer.Id} not exist.")
    //            .WithName("Designer");
    //    }
    //}
}
