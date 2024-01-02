using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Diagnostics;

namespace SODP.UI.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : AppPageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorModel(IWebAPIProvider provider, ILogger<ErrorModel> logger, LanguageTranslatorFactory translatorFactory) : base(provider, logger, translatorFactory) { }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
