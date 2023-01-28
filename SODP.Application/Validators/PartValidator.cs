using FluentValidation;
using SODP.Model;

namespace SODP.Application.Validators
{
    public class PartValidator : AbstractValidator<Part> 
    {
        public PartValidator() 
        {  
            RuleFor(c => c.Sign).NotEmpty();
        }
    }
}
