using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Stages.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages;

[Authorize(Roles = "ProjectManager")]
public sealed class IndexModel : CollectionPageModel
{
	const string _editStageModalViewName = "ModalView/_EditStageModalView";

	public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
	{
		ReturnUrl = "/Stages";
		_endpoint = "stages";
	}


	public IReadOnlyCollection<StageVM> Stages { get; set; }


	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync<Page<StageDTO>>(endpoint);

		Stages = _mapper.Map<IReadOnlyCollection<StageVM>>(apiResponse.Value.Collection);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	public async Task<PartialViewResult> OnGetEditStageAsync(int? id)
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

		return GetPartialView(_mapper.Map<StageVM>(apiResponse.Value), _editStageModalViewName);
	}

	public async Task<PartialViewResult> OnPostEditStageAsync(StageVM stage)
	{
		if (ModelState.IsValid)
		{
			return GetPartialView(stage, _editStageModalViewName);
		}

		HttpResponseMessage apiResponse;

		var content = stage.ToHttpContent();
		apiResponse = stage.Id == 0
			? await _apiProvider.PostAsync($"{_endpoint}", content)
			: await _apiProvider.PatchAsync($"{_endpoint}/{stage.Id}", content);

		if (!apiResponse.IsSuccessStatusCode)
		{
			var response = apiResponse.Content.ReadAsAsync<ApiResponse>();
			// SetModelErrors(response);
		}

		return GetPartialView(stage, _editStageModalViewName);

	}
}
