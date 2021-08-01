using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Designers.ViewModels;
using SODP.UI.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        const string partialViewName = "_NewDesignerPartialView";
        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/Designers";
            _apiProvider = apiProvider;
        }

        public DesignersVM Designers { get; set; }

        public LicencesVM Licences { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0)
        {
            var url = new StringBuilder();
            url.Append(ReturnUrl);
            url.Append("?currentPage=:&pageSize=");
            url.Append(pageSize.ToString());

            Designers = new DesignersVM
            {
                PageInfo = new PageInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = pageSize,
                    Url = url.ToString()
                },
            };

            Designers.Designers = await GetDesignersAsync(Designers.PageInfo);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _apiProvider.DeleteAsync($"/designers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            return RedirectToPage($"Index?currentPage={Designers.PageInfo.CurrentPage}:&pageSize={Designers.PageInfo.ItemsPerPage}");
        }

        public PartialViewResult OnGetNewDesigner()
        {
            return GetPartialView<NewDesignerVM>(new NewDesignerVM(), "_NewDesignerPartialView");
        }

        public async Task<PartialViewResult> OnPostNewDesignerAsync(NewDesignerVM designer)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync($"/designers", designer.ToHttpContent());

                var response = await _apiProvider.GetContent<ServiceResponse<DesignerDTO>>(apiResponse);
                
                if (apiResponse.IsSuccessStatusCode && response.Success)
                {

                    return GetPartialView<NewDesignerVM>(new NewDesignerVM
                    {
                        Title = designer.Title,
                        Firstname = designer.Firstname,
                        Lastname = designer.Lastname
                    }, "_NewDesignerPartialView");
                }
                else
                {
                    SetModelErrors(response);
                }
            }

            return GetPartialView<NewDesignerVM>(designer, "_NewDesignerPartialView");
        }

        public async Task<PartialViewResult> OnGetPartialLicences(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"/designers/{id}/licences");
            var response = await _apiProvider.GetContent<ServicePageResponse<LicenceWithBranchesDTO>>(apiResponse);
            if (apiResponse.IsSuccessStatusCode)
            {
                Licences = new LicencesVM
                {
                    Licences = response.Data.Collection.ToList()
                };
            }

            return GetPartialView<LicencesVM>(Licences, "_LicencesPartialView");
        }

        private async Task<IList<DesignerDTO>> GetDesignersAsync(PageInfo pageInfo)
        {
            var apiResponse = await _apiProvider.GetAsync($"/designers?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await _apiProvider.GetContent<ServicePageResponse<DesignerDTO>>(apiResponse);
                pageInfo.TotalItems = response.Data.TotalCount;
                pageInfo.CurrentPage = response.Data.PageNumber;
                
                return response.Data.Collection.ToList();
            }

            return new List<DesignerDTO>();
        }
    }
}
