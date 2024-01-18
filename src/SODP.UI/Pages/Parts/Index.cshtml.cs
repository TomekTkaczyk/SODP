using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.Extensions;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Parts;

[Authorize(Roles = "ProjectManager")]
public sealed class IndexModel : CollectionPageModel
{
    const string _editPartModalViewName = "ModalView/_EditPartModalView";

    public IndexModel(
        IWebAPIProvider apiProvider,
        ILogger<IndexModel> logger,
        LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
    {
        ReturnUrl = "/Parts";
        _endpoint = "parts";
    }

    public ICollection<PartVM> Parts { get; set; }

    public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
    {
        var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
        var apiResponse = await GetApiResponseAsync<Page<PartDTO>>(endpoint);

        if (!apiResponse.IsSuccess)
        {
            RedirectToPage($"Errors/{apiResponse.HttpCode}");
        }

        Parts = apiResponse.Value.Collection.Select(x => x.ToPartVM()).ToList();
        PageInfo = GetPageInfo(apiResponse, searchString);

        return Page();
    }

    public async Task<IActionResult> OnGetEditPartAsync(int? id)
    {
        if (!id.HasValue)
        {
            return GetPartialView(new PartVM(), _editPartModalViewName);
        }

        var apiResponse = await GetApiResponseAsync<PartDTO>($"{_endpoint}/{id}");

        if (!apiResponse.IsSuccess)
        {
            RedirectToPage($"Errors/{apiResponse.HttpCode}");
        }

        return GetPartialView(apiResponse.Value.ToPartVM(), _editPartModalViewName);
    }

    public async Task<IActionResult> OnPostEditPartAsync(PartVM model)
    {
        if (ModelState.IsValid)
        {
            var responseMessage = model.Id == 0
                ? await _apiProvider.PostAsync($"{_endpoint}", model.ToHttpContent())
                : await _apiProvider.PutAsync($"{_endpoint}/{model.Id}", model.ToHttpContent());

            if (!responseMessage.IsSuccessStatusCode)
            {
                // SetError
            }
        }

        return GetPartialView(model, _editPartModalViewName);
    }
}
