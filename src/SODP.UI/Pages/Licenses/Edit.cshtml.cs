using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Licenses.ViewModels;
using SODP.UI.Pages.Shared.Extensions;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Licenses;

[Authorize(Roles = "ProjectManager")]
public class EditModel : AppPageModel
{
	public EditModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory) { }

	public LicenseVM License { get; set; }

	public async Task<IActionResult> OnGetAsync(int id)
	{
		await GetLicenseAsync(id);

		return Page();
	}

	public async Task<IActionResult> OnPostAsync(LicenseVM license)
	{
		if (ModelState.IsValid)
		{
			var apiResponse = await _apiProvider.PatchAsync($"licenses/{license.Id}", license.ToHttpContent());
			switch (apiResponse.StatusCode)
			{
				case HttpStatusCode.NoContent:
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

		return Redirect("/Designers/index");
	}

	public async Task<IActionResult> OnDeleteBranchAsync(int id, int branchId, string content)
	{
		var apiResponse = await _apiProvider.DeleteAsync($"licenses/{id}/branches/{branchId}");

		await GetLicenseAsync(id);
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

		await GetLicenseAsync(id);

		return Page();
	}

	private async Task GetLicenseAsync(int id)
	{
		var apiResponse = await _apiProvider.GetAsync($"licenses/{id}/branches");
		var response = await _apiProvider.GetContentAsync<ApiResponse<LicenseDTO>>(apiResponse);
		if (apiResponse.IsSuccessStatusCode)
		{
			License = new LicenseVM
			{
				Id = response.Value.Id,
				DesignerId = response.Value.Designer.Id,
				Designer = response.Value.Designer.ToDesignerVM().Name,
				Content = response.Value.Content,
				ApplyBranches = response.Value.Branches
				.OrderBy(x => x.Order)
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Title
				}).ToList()
			};

			License.Branches = await GetBranchesAsync(License.ApplyBranches);
		}
	}

	private async Task<List<SelectListItem>> GetBranchesAsync(List<SelectListItem> exclusionList)
	{
		var apiResponse = await _apiProvider.GetAsync($"branches");
		var responseBranch = await _apiProvider.GetContentAsync<ApiResponse<Page<BranchDTO>>>(apiResponse);
		var result = responseBranch.Value.Collection
			.OrderBy(x => x.Order)
			.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Title.ToString()
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
		var license = new LicenseVM
		{
			Id = License.Id,
			Content = License.Content
		};
		var designer = new DesignerVM
		{
			Id = License.DesignerId
		};

		license.Designer = designer.ToString();

		return license.ToHttpContent();
	}
}
