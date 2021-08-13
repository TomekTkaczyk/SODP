using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Mappers;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class EditModel : SODPPageModel
    {
        private readonly IWebAPIProvider _apiProvider;
        private readonly IMapper _mapper;

        public EditModel(IWebAPIProvider apiProvider, IMapper mapper)
        {
            _apiProvider = apiProvider;
            _mapper = mapper;
        }

        public ProjectVM Project { get; set; }

        public IEnumerable<SelectListItem> Stages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"/active-projects/{id}");
            var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);

            if (apiResponse.IsSuccessStatusCode && response.Success)
            {
                Project = _mapper.Map<ProjectVM>(response.Data);
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
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ProjectVM project)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PutAsync($"/active-projects/{project.Id}", project.ToHttpContent());
                if (apiResponse.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }

                var response = await _apiProvider.GetContent<ServiceResponse>(apiResponse);
                if (!string.IsNullOrEmpty(response.Message))
                {
                    ModelState.AddModelError("", response.Message);
                }
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
          
            return Page();
        }
    }
}
