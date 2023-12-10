using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class AppPageModel : PageModel
{
	protected string _endpoint;

	protected readonly IWebAPIProvider _apiProvider;
	protected readonly ILogger<AppPageModel> _logger;
	protected readonly ITranslator _translator;
	protected readonly IMapper _mapper;

	protected string ReturnUrl { get; set; }

	protected AppPageModel(
		IWebAPIProvider apiProvider,
		ILogger<AppPageModel> logger,
		LanguageTranslatorFactory translatorFactory,
		IMapper mapper)
	{
		_apiProvider = apiProvider;
		_logger = logger;
		_mapper = mapper;
		_translator = translatorFactory.GetTranslator();
	}


    protected virtual void SetModelErrors(HttpResponseMessage message) 
	{
        ModelState.AddModelError("",_translator.Translate(message.StatusCode.ToString()));
    }

    protected virtual void SetModelErrors(ApiResponse response)
	{
		if (!string.IsNullOrEmpty(response.Message))
		{
			ModelState.AddModelError("", _translator.Translate(response.Message));
		}

		foreach (var error in response.Errors)
		{
			ModelState.AddModelError("", _translator.Translate(error.Message));
		}
	}

	protected virtual PartialViewResult GetPartialView<T>(T model, string partialViewName)
	{
		return new PartialViewResult()
		{
			ViewName = partialViewName,
			ViewData = new ViewDataDictionary<T>(ViewData, model)
		};
	}

	protected async Task<ApiResponse<TValue>> GetApiResponseAsync<TValue>(string url)
	{
		var apiResponse = await _apiProvider.GetAsync(url);

		return await apiResponse.Content.ReadAsAsync<ApiResponse<TValue>>();
	}

	protected async Task<ApiResponse<TValue>> PostApiResponseAsync<TValue>(string url, StringContent content)
	{
		var apiResponse = await _apiProvider.PostAsync(url, content);

		return await apiResponse.Content.ReadAsAsync<ApiResponse<TValue>>();
	}

	protected async Task<ApiResponse> PatchApiResponseAsync(string url, StringContent content)
	{
		var apiResponse = await _apiProvider.PatchAsync(url, content);

		return await apiResponse.Content.ReadAsAsync<ApiResponse>();
	}

	protected async Task<ApiResponse> PutApiResponseAsync(string url, StringContent content)
	{
		var apiResponse = await _apiProvider.PutAsync(url, content);

		return await apiResponse.Content.ReadAsAsync<ApiResponse>();
	}

	protected async Task<ApiResponse> DeleteApiResponseAsync(string url, StringContent content)
	{
		var apiResponse = await _apiProvider.DeleteAsync(url);

		return await apiResponse.Content.ReadAsAsync<ApiResponse>();
	}
}

