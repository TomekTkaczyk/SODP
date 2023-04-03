using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
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
	protected readonly IMapper _mapper;
	protected readonly ITranslator _translator;

	protected string ReturnUrl { get; set; }

	protected AppPageModel(
		IWebAPIProvider apiProvider,
		ILogger<AppPageModel> logger,
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory)
	{
		_apiProvider = apiProvider;
		_logger = logger;
		_mapper = mapper;
		_translator = translatorFactory.GetTranslator();
	}

	protected virtual void SetModelErrors(ApiResponse response)
	{
		if (!string.IsNullOrEmpty(response.Message))
		{
			ModelState.AddModelError("", response.Message);
		}

		foreach (var error in response.Errors)
		{
			ModelState.AddModelError(error.Code, error.Message);
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

	protected StringContent GetRequestContent<T>(T obj)
	{
		return new StringContent(
				  JsonSerializer.Serialize(obj),
				  Encoding.UTF8,
				  "application/json"
			  );
	}

	protected async Task<ApiResponse<TValue>> GetApiResponseAsync<TValue>(string url)
	{
		var apiResponse = await _apiProvider.GetAsync(url);

		var content = await apiResponse.Content.ReadAsAsync<ApiResponse<TValue>>();

		return content;
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

