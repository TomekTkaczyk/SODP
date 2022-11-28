using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;

namespace SODP.UI.Pages.ActiveProjects
{
    public class TestModel : SODPPageModel
    {
        public TestModel(ILogger<SODPPageModel> logger, IMapper mapper, ITranslator translator) : base(logger, mapper, translator)
        {
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetTestModal()
        {
            return GetPartialView(new TestModalVM(), "_TestModalPartialView");
        }

        public IActionResult OnPostTestModal(TestModalVM result)
        {
            return GetPartialView(result, "_TestModalPartialView");
        }
    }
}
