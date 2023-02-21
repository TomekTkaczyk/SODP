using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels
{
    public abstract class ProjectsPageModel : ListPageModel<ProjectDTO>
    {

        public ProjectsVM Projects { get; set; }

        protected ProjectsPageModel(IWebAPIProvider apiProvider, ILogger<ProjectsPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            _endpoint = "projects";
        }

        protected async Task<IActionResult> OnGetAsync(ProjectStatus status, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var endpoint = GetUrl(currentPage, pageSize, searchString);
            endpoint += $"&status={status}";
            var apiResponse = await GetApiResponseAsync(endpoint);

            PageInfo = GetPageInfo(apiResponse, searchString);
            Projects = new ProjectsVM
            {
                Projects = apiResponse.Data.Collection.ToList(),
                PageInfo = PageInfo
            };

            return Page();
        }
    }
}
