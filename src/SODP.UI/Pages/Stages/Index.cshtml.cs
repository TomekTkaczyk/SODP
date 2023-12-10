using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Stages.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages;

[Authorize(Roles = "ProjectManager")]
public sealed class IndexModel : CollectionPageModel
{
	const string _editStageModalViewName = "ModalView/_EditStageModalView";

	public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		LanguageTranslatorFactory translatorFactory,
		IMapper mapper) : base(apiProvider, logger, translatorFactory, mapper)
	{
		ReturnUrl = "/Stages";
		_endpoint = "stages";
	}

	public IReadOnlyCollection<StageVM> Stages { get; set; }

	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync<Page<StageVM>>(endpoint);
		
		if (!apiResponse.IsSuccess)
		{
			RedirectToPage($"Errors/{apiResponse.HttpCode}");
		}

		Stages = _mapper.Map<IReadOnlyCollection<StageVM>>(apiResponse.Value.Collection);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	public async Task<IActionResult> OnGetEditStageAsync(int? id)
	{
		if (!id.HasValue)
		{
			return GetPartialView(new StageVM(), _editStageModalViewName);
		}

		var apiResponse = await GetApiResponseAsync<StageVM>($"{_endpoint}/{id}");

		if (!apiResponse.IsSuccess)
		{
			RedirectToPage($"Errors/{apiResponse.HttpCode}");
		}

		return GetPartialView(_mapper.Map<StageVM>(apiResponse.Value), _editStageModalViewName);
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
