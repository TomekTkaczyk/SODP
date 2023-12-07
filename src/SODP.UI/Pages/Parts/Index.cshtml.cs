using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Parts.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Parts;

[Authorize(Roles = "ProjectManager")]
public sealed class IndexModel : CollectionPageModel
{
    const string _editPartModalViewName = "ModalView/_EditPartModalView";

    public IndexModel(
        IWebAPIProvider apiProvider,
        ILogger<IndexModel> logger,
        IMapper mapper,
        LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
    {
        ReturnUrl = "/Parts";
        _endpoint = "parts";
    }

    public IReadOnlyCollection<PartVM> Parts { get; set; }

    public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
    {
        var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
        var apiResponse = await GetApiResponseAsync<Page<PartDTO>>(endpoint);

        if (!apiResponse.IsSuccess)
        {
            RedirectToPage($"Errors/{apiResponse.HttpCode}");
        }

        Parts = _mapper.Map<IReadOnlyCollection<PartVM>>(apiResponse.Value.Collection);
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

        return GetPartialView(_mapper.Map<PartVM>(apiResponse.Value), _editPartModalViewName);
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
