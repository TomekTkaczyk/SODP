using Microsoft.Extensions.Logging;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;

namespace SODP.UI.Pages
{
	public class IndexModel : AppPageModel
    {
        public IndexModel(IWebAPIProvider provider, ILogger<IndexModel> logger, LanguageTranslatorFactory translatorFactory) : base(provider, logger, translatorFactory) { } 
    }
}
