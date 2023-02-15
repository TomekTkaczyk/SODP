using FluentValidation;
using SODP.Model;

namespace SODP.Application.Validators
{
    public class DictionaryValidator : AbstractValidator<AppDictionary>
    {
        public DictionaryValidator() 
        {
            RuleFor(c => c.Sign)
                .NotEmpty().WithMessage("Sign cannot be null or empty")
                .Length(1, 10).WithMessage("The maximum length is 10 characters");
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Sign cannot be null or empty")
                .Length(1, 50).WithMessage("The maximum length is 50 characters");
        }
    }
}
