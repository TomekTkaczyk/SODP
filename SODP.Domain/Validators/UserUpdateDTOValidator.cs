using FluentValidation;
using SODP.Shared.DTO;

namespace SODP.Domain.Validators
{
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id jest wymagany.");
        }
    }
}
