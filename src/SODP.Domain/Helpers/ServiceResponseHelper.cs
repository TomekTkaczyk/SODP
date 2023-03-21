using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.Domain.Helpers
{
    public static class ServiceResponseHelper
    {
        public static void ValidationErrorProcess(this ServiceResponse response, ValidationResult validationResult)
        {
            foreach(var item in validationResult.Errors)
            {
                response.ValidationErrors.Add(new KeyValuePair<string, string>(item.PropertyName, item.ErrorMessage));
            }
            response.StatusCode = 400;
            response.Success = false;
        }

        public static void IdentityResultErrorProcess(this ServiceResponse response, IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
            {
                response.SetError($"{error.Code}: {error.Description}");
            }
            response.Success = false;
        }
    }

}
