using FluentValidation;
using SODP.Model;

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
