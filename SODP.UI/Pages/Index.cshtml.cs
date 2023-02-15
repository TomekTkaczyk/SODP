using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;

namespace SODP.UI.Pages
{
    public class IndexModel : SODPPageModel
    {
        public IndexModel(ILogger<SODPPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(logger, mapper, translatorFactory) { } 
    }
}
