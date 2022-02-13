using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SODP.UI.Pages.Shared;

namespace SODP.UI.Pages
{
    public class IndexModel : SODPPageModel
    {
        public IndexModel(ILogger<SODPPageModel> logger) : base(logger) { } 
    }
}
