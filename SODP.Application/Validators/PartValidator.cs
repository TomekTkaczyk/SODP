using FluentValidation;
using SODP.Domain.Entities;

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
