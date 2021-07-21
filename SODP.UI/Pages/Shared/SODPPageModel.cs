using Microsoft.AspNetCore.Mvc.RazorPages;
using SODP.Shared.Response;

namespace SODP.UI.Pages.Shared
{
    public abstract class SODPPageModel : PageModel
    {
        protected string PartialViewName { get; set; }

        public string ReturnUrl { get; protected set; }

        protected virtual void SetModelErrors(ServiceResponse response)
        {
            if (!string.IsNullOrEmpty(response.Message))
            {
                ModelState.AddModelError("", response.Message);
            }
            foreach (var error in response.ValidationErrors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}
