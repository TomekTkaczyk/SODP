using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Mappers;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
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

        public DesignersListVM DesignersViewModel { get; set; }

        public LicencesListVM LicencesViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 15)
        {
            var url = new StringBuilder();
            url.Append(ReturnUrl);
            url.Append("?currentPage=:&pageSize=");
            url.Append(pageSize.ToString());

            LicencesViewModel = new LicencesListVM
            {
                Licences = new List<LicenceDTO>()
            };

            DesignersViewModel = new DesignersListVM
            {
                PageInfo = new PageInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = pageSize,
                    Url = url.ToString()
                },
            };

            DesignersViewModel.Designers = await GetDesignersAsync(DesignersViewModel.PageInfo);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _apiProvider.DeleteAsync($"/designers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            return RedirectToPage($"Index?currentPage={DesignersViewModel.PageInfo.CurrentPage}:&pageSize={DesignersViewModel.PageInfo.ItemsPerPage}");
        }

        public PartialViewResult OnGetNewDesigner()
        {
            return GetPartialView(new NewDesignerVM());
        }

        public async Task<PartialViewResult> OnPostNewDesignerAsync(NewDesignerVM designer)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync($"/designers", designer.ToHttpContent());

                var response = await _apiProvider.GetContent<ServiceResponse<DesignerDTO>>(apiResponse);
                
                if (apiResponse.IsSuccessStatusCode && response.Success)
                {
                    return GetPartialView(new NewDesignerVM
                    {
                        Title = designer.Title,
                        Firstname = designer.Firstname,
                        Lastname = designer.Lastname
                    });
                }
                else
                {
                    SetModelErrors(response);
                }
            }

            return GetPartialView(designer);
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

        private PartialViewResult GetPartialView(NewDesignerVM designer)
        {
            return new PartialViewResult
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewDesignerVM>(ViewData, designer)
            };
        }

        public async Task<PartialViewResult> OnGetPartialLicences(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"/designers/{id}/licences");
            var response = await _apiProvider.GetContent<ServicePageResponse<LicenceDTO>>(apiResponse);
            if (apiResponse.IsSuccessStatusCode)
            {
                LicencesViewModel = new LicencesListVM
                {
                    DesignerId = 1,
                    Licences = response.Data.Collection.ToList()
                };
            }

            return new PartialViewResult
            {
                ViewName = "_LicencesPartialView",
                ViewData = new ViewDataDictionary<LicencesListVM>(ViewData, LicencesViewModel)
            };
        }
    }
}
