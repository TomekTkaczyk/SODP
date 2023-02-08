using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using SODP.UI.ViewModels;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects
{
    [Authorize(Roles = "Administrator,ProjectManager,User")]
    public class EditModel : ProjectEditPageModel
    {

        public EditModel(IWebAPIProvider apiProvider, ILogger<EditModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory) { }

        public ProjectVM Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"projects/{id}");
            var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);

            if (apiResponse.IsSuccessStatusCode && response.Success)
            {
                Project = new ProjectVM
                {
                  Id = response.Data.Id,
                  Number = response.Data.Number,
                  StageId = response.Data.Stage.Id,
                  StageSign = response.Data.Stage.Sign,
                  StageName = response.Data.Stage.Name,
                  Title = response.Data.Title,
                  Description = response.Data.Description,
                  Status = response.Data.Status
                };
            }
            else
            {
                if (!string.IsNullOrEmpty(response.Message))
                {
                    ModelState.AddModelError("", response.Message);
                }
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                Project = new ProjectVM
                {
                    Status = ProjectStatus.Archival
                };
            }

            return Page();
        }
    }
}
