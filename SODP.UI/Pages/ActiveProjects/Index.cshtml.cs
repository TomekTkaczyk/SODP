using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
	[Authorize(Roles = "User, ProjectManager")]
    public class IndexModel : ProjectsPageModel
    {
        const string _newProjectModalViewName = "ModalView/_NewProjectModalView";
        const string _projectPartialViewName = "PartialView/_ProjectPartialView";

        public ProjectVM Project { get; set; }


        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/ActiveProjects";
        }


        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            return await OnGetAsync(ProjectStatus.Active, currentPage, pageSize, searchString);
        }


		public async Task<IActionResult> OnGetNewProjectAsync()
        {
            return await GetNewProjectPartialViewAsync(new NewProjectVM());
        }


        public async Task<IActionResult> OnPostNewProjectAsync(NewProjectVM project)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync(_endpoint, project.ToHttpContent());
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);
                        project.Id = response.Data.Id;
                        if (!response.Success)
                        {
                            SetModelErrors(response);
                        }
                        break;
                    default:
                        break;
                }
            }
			
            return GetPartialView(project, _newProjectModalViewName);
		}


		public async Task<IActionResult> OnGetProjectPartialAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"projects/{id}/details");
            var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);
            Project = _mapper.Map<ProjectVM>(response.Data);

			return GetPartialView(Project, _projectPartialViewName);
        }


        private async Task<IActionResult> GetNewProjectPartialViewAsync(NewProjectVM project)
        {
            var apiResponse = await _apiProvider.GetAsync("stages");
            if (apiResponse.IsSuccessStatusCode)
            {
                var stages = await _apiProvider.GetContent<ServicePageResponse<StageDTO>>(apiResponse);
                project.Stages = stages.Data.Collection
                    .Where(x => x.ActiveStatus)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.ToString()
                    }).ToList();
            }

			return GetPartialView(project, _newProjectModalViewName);
        }
	}
}
