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
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class EditModel : ProjectEditPageModel
    {
        const string technicalRolesPartialViewName = "_TechnicalRolesPartialView";

        public EditModel(IWebAPIProvider apiProvider, ILogger<EditModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator) { }

        public ProjectVM Project { get; set; }

        public TechnicalRoleVM TechnicalRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project  = await GetProjectAsync(id);

            if(Project == null)
            {
                return Redirect("/Errors/404");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ProjectVM project)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PutAsync($"projects/{project.Id}", project.ToHttpContent());
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

        public async Task<IActionResult> OnPutBranchAsync(int id, int branchId)
        {
            var apiResponse = await _apiProvider.PutAsync($"projects/{id}/branches/{branchId}", new StringContent(
                                  JsonSerializer.Serialize(branchId),
                                  Encoding.UTF8,
                                  "application/json"
                              ));

            Project = await GetProjectAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnDeleteBranchAsync(int id, int branchId)
        {
            await _apiProvider.DeleteAsync($"projects/{id}/branches/{branchId}");

            Project = await GetProjectAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnGetTechnicalRolesAsync(int projectId, int branchId)
        {
            return await GetPartialViewAsync(projectId, branchId);
        }

        public async Task<IActionResult> OnPostTechnicalRolesAsync(TechnicalRoleVM role)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PatchAsync($"projects/{role.ProjectId}/branch/{role.BranchId}/role/{role.RoleId}",
                    role.ToHttpContent());

                var response = await _apiProvider.GetContent<ServiceResponse<object>>(apiResponse);

                if (apiResponse.IsSuccessStatusCode && response.Success)
                {
                    return await GetPartialViewAsync(role.ProjectId, role.BranchId);
                }
                else
                {
                    SetModelErrors(response);
                }
            }
            
            return await GetPartialViewAsync(role.ProjectId, role.BranchId);
        }

        private async Task<PartialViewResult> GetPartialViewAsync(int projectId, int branchId)
        {
            var apiResponse = await _apiProvider.GetAsync($"projects/{projectId}/branches/{branchId}");
            var responseRoles = await _apiProvider.GetContent<ServicePageResponse<ProjectBranchRoleDTO>>(apiResponse);

            var technicalRoles = new TechnicalRoleVM
            {
                ProjectId = projectId,
                BranchId = branchId,

                AvailableRoles = Enum.GetValues(typeof(TechnicalRole))
                .Cast<TechnicalRole>()
                .Select(x => new SelectListItem
                {
                    Value = ((int)x).ToString(),
                    Text = _translator.Translate(x.ToString(), Languages.Pl)
                }).ToList(),

                Roles = responseRoles.Data.Collection
                .Where(y => Enum.TryParse(y.Role, out TechnicalRole role))
                .Select(x =>
                {
                    Enum.TryParse(x.Role, out TechnicalRole role);
                    return new
                    {
                        key = role,
                        license = new LicenseVM()
                        {
                            Content = x.License.Content,
                            Designer = x.License.Designer.ToString()
                        }
                    };
                }).ToDictionary(t => t.key, t => t.license)
            };

            apiResponse = await _apiProvider.GetAsync($"licenses/branch/{branchId}");
            var responseLicenses = await _apiProvider.GetContent<ServicePageResponse<LicenseDTO>>(apiResponse);

            technicalRoles.Licenses = responseLicenses.Data.Collection
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Designer} ({x.Content})"
                }).ToList();
            technicalRoles.Licenses.Add(new SelectListItem("-- usuñ z listy --", "0"));

            return GetPartialView(technicalRoles, technicalRolesPartialViewName);
        }

    }
}
