using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        LanguageTranslatorFactory translatorFactory,
        IMapper mapper) : base(apiProvider, logger, translatorFactory, mapper)
    {
        ReturnUrl = "/Parts";
        _endpoint = "parts";
    }

    public ICollection<PartVM> Parts { get; set; }

    public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
    {
        var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
        var apiResponse = await GetApiResponseAsync<Page<PartVM>>(endpoint);

        if (!apiResponse.IsSuccess)
        {
            RedirectToPage($"Errors/{apiResponse.HttpCode}");
        }

        Parts = _mapper.Map<ICollection<PartVM>>(apiResponse.Value.Collection);
        PageInfo = GetPageInfo(apiResponse, searchString);

        return Page();
    }

    public async Task<IActionResult> OnGetEditPartAsync(int? id)
    {
        if (!id.HasValue)
        {
            return GetPartialView(new PartVM(), _editPartModalViewName);
        }

        var apiResponse = await GetApiResponseAsync<PartVM>($"{_endpoint}/{id}");

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
