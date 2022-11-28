using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Licenses.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace SODP.UI.Pages.Licenses
{
    public class EditModel : SODPPageModel
    {
        private readonly IWebAPIProvider _apiProvider;

        public EditModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, ITranslator translator) : base(logger, mapper, translator)
        {
            _apiProvider = apiProvider;
            var prev = Request;
        }

        [BindProperty]
        public LicenseVM License { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            await GetLicenceAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostEditLicenseAsync()
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PutAsync($"licenses/{License.Id}", this.ToHttpContent());
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse>(apiResponse);
                        if (!response.Success)
                        {
                            SetModelErrors(response);
                        }
                        break;
                    default:
                        return Redirect($"/Errors/{apiResponse.StatusCode}");
                }
            }
            else
            {
                License.Branches = await GetBranchesAsync(License.ApplyBranches);

                return Page();
            }

            return Redirect($"/Designers/index?handler=LicensesPartial&id={License.DesignerId}");

        }

        public async Task<IActionResult> OnDeleteBranchAsync(int id, int branchId, string content)
        {
            await _apiProvider.DeleteAsync($"licenses/{id}/branches/{branchId}");

            await GetLicenceAsync(id);

            License.Content = content;

            return Page();

        }

        public async Task<IActionResult> OnPutBranchAsync(int id, int branchId)
        {
            await _apiProvider.PutAsync($"licenses/{id}/branches/{branchId}", new StringContent(
                                  JsonSerializer.Serialize(branchId),
                                  Encoding.UTF8,
                                  "application/json"
                              ));

            await GetLicenceAsync(id);

            return Page();
        }

        private async Task GetLicenceAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"licenses/{id}/branches");
            var response = await _apiProvider.GetContent<ServiceResponse<LicenseWithBranchesDTO>>(apiResponse);
            if (apiResponse.IsSuccessStatusCode)
            {
                License = new LicenseVM
                {
                    Id = response.Data.Id,
                    DesignerId = response.Data.Designer.Id,
                    Designer = response.Data.Designer.ToString(),
                    Content = response.Data.Content,
                    ApplyBranches = response.Data.Branches
                    .OrderBy(x => x.Order)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.ToString()
                    }).ToList()
                };

                License.Branches = await GetBranchesAsync(License.ApplyBranches);
            }
        }

        private async Task<List<SelectListItem>> GetBranchesAsync(List<SelectListItem> exclusionList)
        {
            var apiResponse = await _apiProvider.GetAsync($"branches");
            var responseBranch = await _apiProvider.GetContent<ServicePageResponse<BranchDTO>>(apiResponse);
            var result = responseBranch.Data.Collection
                .OrderBy(x => x.Order)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                }).ToList();

            if (exclusionList != null)
            {
                var shortList = result.Where(y => !exclusionList.Contains(y, new SelectListItemComparer())).ToList();
                result = shortList;
            }

            return result;
        }

        private StringContent ToHttpContent()
        {
            var licenseDTO = new LicenseDTO
            {
                Id = License.Id,
                Content = License.Content
            };
            licenseDTO.Designer = new DesignerDTO
            {
                Id = License.DesignerId
            };

            return new StringContent(
                                  JsonSerializer.Serialize(licenseDTO),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
