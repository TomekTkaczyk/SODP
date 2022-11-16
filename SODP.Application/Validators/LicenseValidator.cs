using FluentValidation;
using SODP.Domain.Services;
using SODP.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Application.Validators
{
    public class LicenseValidator : AbstractValidator<License>
    {
        private readonly IDesignerService _designerService;
        public LicenseValidator(IDesignerService designerService)
        {
            _designerService = designerService;

            RuleFor(u => u.Content)
                .NotNull()
                .NotEmpty().When(u => u.Content != null, ApplyConditionTo.CurrentValidator)
                .WithMessage("Numer jest wymagany")
                .WithName("Nr uprawnień");

            RuleFor(u => u.Designer)
                .NotNull()
                .WithMessage("Projektant jest wymagany")
                .MustAsync((designer, cancellation) => _designerService.DesignerExist(designer.Id))
                .When(u => u.Designer != null, ApplyConditionTo.CurrentValidator)
                .WithMessage(u => $"Projektant:{u.Designer.Id} nie występuje w bazie.")
                .WithName("Projektant");
        }
    }
}
