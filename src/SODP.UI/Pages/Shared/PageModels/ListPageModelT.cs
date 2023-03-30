//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using SODP.Shared.Response;
//using SODP.UI.Infrastructure;
//using SODP.UI.Services;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace SODP.UI.Pages.Shared.PageModels;

//public abstract class ListPageModel<T> : CollectionPageModel //where T : BaseDTO
//{

//	protected ListPageModel(IWebAPIProvider apiProvider, ILogger<CollectionPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
//	{
//	}


//	protected ListPageModel(
//		IWebAPIProvider apiProvider,
//		ILogger<ListPageModel<T>> logger,
//		IMapper mapper,
//		LanguageTranslatorFactory translatorFactory)
//		: base(apiProvider, logger, mapper, translatorFactory) { }


//	protected async Task<ICollection<T>> GetCollectionAsync(string url)
//	{
//		var apiResponse = await _apiProvider.GetAsync(url);

//		if (apiResponse.IsSuccessStatusCode)
//		{
//			var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<T>>();
//			PageInfo.TotalItems = response.Data.TotalCount;
//			PageInfo.CurrentPage = response.Data.PageNumber;
//			PageInfo.ItemsPerPage = response.Data.PageSize;
//			//			PageInfo.Url = GetUrl(response.Data.PageNumber, response.Data.PageSize, "");

//			return response.Data.Collection;
//		}

//		return new List<T>();
//	}
//}
