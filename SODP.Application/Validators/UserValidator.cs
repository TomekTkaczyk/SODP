using FluentValidation;
using SODP.Domain.Entities;

namespace SODP.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Login jest wymagany.")
                .WithName("Nazwa użytkownika (login)");
        }
    }
}
