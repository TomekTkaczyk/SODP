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

namespace SODP.UI.Pages.Stages;

[Authorize(Roles = "ProjectManager")]
public sealed class IndexModel : CollectionPageModel
{
    const string _editStageModalViewName = "ModalView/_EditStageModalView";

    public IndexModel(
        IWebAPIProvider apiProvider,
        ILogger<IndexModel> logger,
        LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
    {
        ReturnUrl = "/Stages";
        _endpoint = "stages";
    }

    public ICollection<StageVM> Stages { get; set; }

    public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
    {
        var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
        var apiResponse = await GetApiResponseAsync<Page<StageDTO>>(endpoint);

        if (!apiResponse.IsSuccess)
        {
            RedirectToPage($"Errors/{apiResponse.HttpCode}");
        }

        Stages = apiResponse.Value.Collection.Select(x => x.ToStageVM()).ToList();
        PageInfo = GetPageInfo(apiResponse, searchString);

        return Page();
    }

    public async Task<IActionResult> OnGetEditStageAsync(int? id)
    {
        if (!id.HasValue)
        {
            return GetPartialView(new StageVM(), _editStageModalViewName);
        }

        var apiResponse = await GetApiResponseAsync<StageDTO>($"{_endpoint}/{id}");

        if (!apiResponse.IsSuccess)
        {
            RedirectToPage($"Errors/{apiResponse.HttpCode}");
        }

        return GetPartialView(apiResponse.Value.ToStageVM(), _editStageModalViewName);
    }

    public async Task<IActionResult> OnPostEditStageAsync(StageVM model)
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

        return GetPartialView(model, _editStageModalViewName);

    }
}
