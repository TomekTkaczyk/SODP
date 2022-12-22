using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System.Collections.Generic;

namespace SODP.UI.Pages.Shared
{
    public abstract class ListPageModel : SODPPageModel
    {
        protected readonly IWebAPIProvider _apiProvider;
        protected string _endpoint;

        public string SearchString { get; set; }
        
        public PageInfo PageInfo { get; set; } = new PageInfo();

        public List<SelectListItem> PageSizeList { get; set; } = new List<SelectListItem> { };

        public int PageSize { get; set; }

        protected ListPageModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, ITranslator translator) : base(logger, mapper, translator) 
        { 
            _apiProvider = apiProvider;
            foreach (var item in PageSizeSelectList.PageSizeList)
            {
                PageSizeList.Add(new SelectListItem(item.ToString(), item.ToString()));
            }
        }
    }
}
