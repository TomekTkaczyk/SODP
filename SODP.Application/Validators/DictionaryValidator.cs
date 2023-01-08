using FluentValidation;
using SODP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Application.Validators
{
    public class DictionaryValidator : AbstractValidator<AppDictionary>
    {
        public DictionaryValidator() 
        {
            RuleFor(c => c.Sign).NotEmpty();
        }
    }
}
