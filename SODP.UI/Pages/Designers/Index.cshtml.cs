using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Shared.DTO;

namespace SODP.UI.Pages.Designers
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public PartialViewResult OnGetDesignerDetails()
        {
            var partialViewResult = new PartialViewResult
            {
                ViewName = "_DesignerPartialView",
                ViewData = new ViewDataDictionary<DesignerDTO>(ViewData, new DesignerDTO())
            };

            return partialViewResult;
        }

        public void OnPostDesignerDetails()
        {

        }
    }
}
