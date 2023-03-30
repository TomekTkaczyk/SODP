using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class AppPageModel : PageModel
{
	protected string _endpoint;

	protected readonly IWebAPIProvider _apiProvider;
	protected readonly ILogger<AppPageModel> _logger;
	protected readonly IMapper _mapper;
	public readonly ITranslator _translator;

	public string ReturnUrl { get; protected set; }

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

	protected async Task<ApiResponse<TValue>> GetApiResponseAsync<TValue>(string url)
	{
		var apiResponse = await _apiProvider.GetAsync(url);

		var content = await apiResponse.Content.ReadAsAsync<ApiResponse<TValue>>();

		return content;
	}

}

