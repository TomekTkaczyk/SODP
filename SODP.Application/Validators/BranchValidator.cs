using FluentValidation;
using SODP.Domain.Entities;

namespace SODP.Application.Validators
{
    public class BranchValidator : AbstractValidator<Branch> 
    {
        public BranchValidator() 
        {  
            RuleFor(c => c.Sign).NotEmpty();
        }
    }
}
