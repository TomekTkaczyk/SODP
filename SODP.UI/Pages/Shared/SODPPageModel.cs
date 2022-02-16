using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;

namespace SODP.UI.Pages.Shared
{
    public abstract class SODPPageModel : PageModel
    {
        private readonly ILogger<SODPPageModel> _logger;

        protected string PartialViewName { get; set; }

        public string ReturnUrl { get; protected set; }

        protected SODPPageModel(ILogger<SODPPageModel> logger)
        {
            _logger = logger;
        }

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

        protected virtual PartialViewResult GetPartialView<T>(T model, string partialViewName)
        {
            return new PartialViewResult()
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<T>(ViewData, model)
            };
        }
    }
}
