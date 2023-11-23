using FluentValidation;
using SODP.Domain.Entities;

namespace SODP.Application.Validators
{
    public class StageValidator : AbstractValidator<Stage>
    {
        public StageValidator()
        {
            RuleFor(x => x.Sign.Value)
                .NotNull()
                .NotEmpty()
                .WithMessage("Oznaczenie stadium jest wymagane.")
                .Matches(@"^([a-zA-Z]{2})([a-zA-Z _]{0,})$")
                .WithMessage("Znak moze zawierać litery i podkreślenie. Na początku minimum 2 litery")
                .WithName("Znak");

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nazwa stadium jest wymagana")
                .WithName("Nazwa");
        }
    }
}
