using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Samples.ViewModels;
using SODP.UI.Pages.Shared;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Samples
{
    public class IndexModel : SODPPageModel
    {
        const string editSamplePartialViewName = "_EditSamplePartialView";

        public IndexModel(ILogger<SODPPageModel> logger) : base(logger)
        {
            ReturnUrl = "/Samples";
        }

        public SamplesVM Sample { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public PartialViewResult OnGetEditSample()
        {
            return GetPartialView(new SamplesVM(), editSamplePartialViewName);
        }
    }
}
