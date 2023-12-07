using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.Extensions;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects;

[Authorize(Roles = "Administrator,ProjectManager")]
public class EditModel : AppPageModel
{
	const string _editProjectPartViewName = "ModalView/_EditProjectPartModalView";
	const string _addTechnicalRoleViewName = "ModalView/_AddTechnicalRoleModalView";
	const string _addPartBranchViewName = "ModalView/_AddPartBranchModalView";
	const string _getInvestorViewName = "ModalView/_GetInvestorModalView";
	const string _partBranchesViewName = "PartialView/_PartBranchesPartialView";

	public EditModel(
		IWebAPIProvider apiProvider,
		ILogger<EditModel> logger,
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory)
		: base(apiProvider, logger, mapper, translatorFactory) 
	{
		_endpoint = "projects";
	}

	public ProjectDetailVM Project { get; set; }

	public InvestorsVM Investors { get; set; }

	public TechnicalRoleVM TechnicalRoles { get; set; }

	public async Task<IActionResult> OnGetAsync(int id)
	{
		var apiResponse = await GetApiResponseAsync<ProjectDetailVM>($"{_endpoint}/{id}/details");

		Project = apiResponse.Value;

		return Project is null 
			? Redirect("/Errors/404") 
			: Page();
	}

	public async Task<IActionResult> OnPostAsync(ProjectDetailVM Project)
	{
		if (ModelState.IsValid)
		{
			var apiResponse = await _apiProvider.PutAsync(
				$"projects/{Project.Id}",
				GetRequestContent(new
				{
					Project.Id,
					Project.Name,
					Project.Title,
					Project.Address,
					Project.LocationUnit,
					Project.BuildingCategory,
					Project.Investor,
					Project.BuildingPermit,
					Project.Description,
					Project.DevelopmentDate
				}));

			if (apiResponse.IsSuccessStatusCode)
			{
				return RedirectToPage("Index");
			}

			var response = await _apiProvider.GetContent<ApiResponse>(apiResponse);
			if (!string.IsNullOrEmpty(response.Message))
			{
				ModelState.AddModelError("", response.Message);
			}

			//foreach (var error in response.ValidationErrors)
			//{
			//	ModelState.AddModelError(error.Key, error.Value);
			//}
		}

		return Page();
	}

	public async Task<PartialViewResult> OnGetInvestorsListAsync(int projectId)
	{
		var _apiResponse = await GetApiResponseAsync<Page<InvestorDTO>>("investors");

		var investors = _apiResponse.Value.Collection
			.Where(x => x.ActiveStatus)
			.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Name
			}).ToList();

		var model = new InvestorsVM()
		{
			ProjectId = projectId,
			Investors = investors
		};

		return GetPartialView(model, _getInvestorViewName);
	}

	public async Task<IActionResult> OnGetEditProjectPartAsync(int projectId, int projectPartId)
	{
		var model = new PartVM()
		{
			Id = projectPartId,
			ProjectId = projectId,
			Items = await GetAvailablePartsAsync(),
		};

		var part = await GetProjectPartAsync(projectPartId);
		if (part is not null)
		{
			model.Sign = part.Sign;
			model.Title = part.Title;
		}

		return GetPartialView(model, _editProjectPartViewName);
	}

	public async Task<IActionResult> OnPostEditProjectPartAsync(PartVM part)
	{
		if (ModelState.IsValid)
		{
			// HttpResponseMessage apiResponse;
			// var aaa = await PostApiResponseAsync<PartDTO>($"projects/{part.ProjectId}/parts", part.ToHttpContent());
			var apiResponse = part.Id == 0
				? await PostApiResponseAsync<PartDTO>($"projects/{part.ProjectId}/parts", part.ToHttpContent())
				: await PutApiResponseAsync($"projects/parts/{part.Id}", part.ToHttpContent());
			
			if (!apiResponse.IsSuccess)
			{
				// SetModelErrors(response);
			}
		} else { 
			part.Items = await GetAvailablePartsAsync();
		}

		return GetPartialView(part, _editProjectPartViewName);
	}

	public async Task<PartialViewResult> OnGetPartBranchesPartialAsync(int projectPartId)
	{
		var model = new ProjectPartVM();

		var apiResponse = await GetApiResponseAsync<ProjectPartDTO>($"projects/parts/{projectPartId}/branches");

		// var apiResponse = await _apiProvider.GetAsync($"projects/parts/{projectPartId}/branches");
		if (apiResponse.IsSuccess)
		{
			//var aaa = apiResponse.Value;
			//var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<ProjectPartDTO>>();
			model = _mapper.Map<ProjectPartVM>(apiResponse.Value);
		}

		return GetPartialView(model, _partBranchesViewName);
	}

	public async Task<PartialViewResult> OnGetAddPartBranchAsync(int projectPartId)
	{
		var model = new AvailableBranchesVM
		{
			ProjectPartId = projectPartId
		};

		var apiResponse = await _apiProvider.GetAsync($"branches?active=true");
		if (apiResponse.IsSuccessStatusCode)
		{
			var result = await apiResponse.Content.ReadAsAsync<ApiResponse<Page<BranchDTO>>>();
			model.Items = result.Value.Collection.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.ToString()
			}).ToList();

			apiResponse = await _apiProvider.GetAsync($"projects/parts/{projectPartId}");
			if (apiResponse.IsSuccessStatusCode)
			{
				var part = await apiResponse.Content.ReadAsAsync<ApiResponse<ProjectPartDTO>>();
				foreach (var item in part.Value.Branches)
				{
					var selected = model.Items.SingleOrDefault(x => x.Value == item.Branch.Id.ToString());
					model.Items.Remove(selected);
				}
			}
		}

		return GetPartialView(model, _addPartBranchViewName);
	}

	public async Task<PartialViewResult> OnPostAddPartBranch(AvailableBranchesVM model)
	{
		model.Items = new List<SelectListItem>();
		if (ModelState.IsValid)
		{
			if (model.SelectedId.HasValue && (model.SelectedId != 0))
			{
				var apiResponse = await _apiProvider.PostAsync($"projects/parts/{model.ProjectPartId}/branches/{model.SelectedId}", new StringContent(""));
				if (apiResponse.StatusCode == HttpStatusCode.OK)
				{
					var response = await _apiProvider.GetContent<ApiResponse>(apiResponse);
					if (!response.IsSuccess)
					{
						// SetModelErrors(response);
					}
				}
			}
		}

		return GetPartialView(model, _addPartBranchViewName);
	}

	public async Task<PartialViewResult> OnGetAddTechnicalRoleAsync(int partBranchId)
	{
		var response = await PartBranchServiceResponseAsync(partBranchId);
		var roles = GetAvailableRoles(response.Value);
		var designers = await GetAvailableDesignersAsync(response.Value);

		var model = new AvailableRolesVM
		{
			PartBranchId = partBranchId,
			ItemsRole = roles,
			ItemsLicense = designers,
		};

		return GetPartialView(model, _addTechnicalRoleViewName);
	}

	public async Task<PartialViewResult> OnPostAddTechnicalRole(AvailableRolesVM model)
	{
		var partBranchResponse = await PartBranchServiceResponseAsync(model.PartBranchId);
		model.ItemsRole = GetAvailableRoles(partBranchResponse.Value);
		model.ItemsLicense = await GetAvailableDesignersAsync(partBranchResponse.Value);
		if (ModelState.IsValid)
		{
			var role = new NewPartBranchRoleDTO
			{
				partBranchId = model.PartBranchId,
				Role = (TechnicalRole)Enum.Parse(typeof(TechnicalRole), model.SelectedRoleId.ToString()),
				LicenseId = (int)model.SelectedLicenseId
			};
			var apiResponse = await _apiProvider.PostAsync($"projects/parts/branches/roles", new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json"));
			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				var response = await _apiProvider.GetContent<ApiResponse>(apiResponse);
				if (!response.IsSuccess)
				{
					// SetModelErrors(response);
				}
			}
		}

		return GetPartialView(model, _addTechnicalRoleViewName);
	}


	private SelectList GetAvailableRoles(PartBranchDTO partBranch)
	{
		var roles = Enum.GetValues(typeof(TechnicalRole)).Cast<TechnicalRole>().Select(x => new SelectListItem
		{
			Value = ((int)x).ToString(),
			Text = x.ToString()
		}).ToList();

		foreach (var item in partBranch.Roles)
		{
			var existRole = roles.SingleOrDefault(x => x.Text.Equals(item.Role));
			if (existRole != null)
			{
				roles.Remove(existRole);
			}
		}

		foreach (var item in roles)
		{
			item.Text = _translator.Translate(item.Text);
		}

		return new SelectList(roles, "Value", "Text");
	}

	private async Task<SelectList> GetAvailableDesignersAsync(PartBranchDTO partBranch)
	{
		var apiResponse = await _apiProvider.GetAsync($"licenses/branches/{partBranch.Branch.Id}");
		var result = await apiResponse.Content.ReadAsAsync<ApiResponse<Page<LicenseDTO>>>();
		var licenses = result.Value.Collection.ToList();
		licenses.RemoveAll(x => partBranch.Roles.Select(x => x.License.Designer).Any(y => y.Id == x.Designer.Id));
		var designers = licenses.Select(x => new SelectListItem
		{
			Value = x.Id.ToString(),
			Text = $"{x.Designer} ({x.Content})"
		}).ToList();

		return new SelectList(designers, "Value", "Text");
	}

	private async Task<ProjectPartDTO> GetProjectPartAsync(int projectPartId)
	{
		var apiResponse = await GetApiResponseAsync<ProjectPartDTO>($"projects/parts/{projectPartId}");

		return apiResponse.IsSuccess 
			? apiResponse.Value 
			: null;
	}

	private async Task<IList<SelectListItem>> GetAvailablePartsAsync()
	{
		var apiResponse = await GetApiResponseAsync<Page<PartDTO>>($"parts?active=true");
		if (apiResponse.IsSuccess)
		{
			return apiResponse.Value.Collection.Select(x => new SelectListItem
			{
				Value = x.Sign,
				Text = x.Title,
			}).ToList();
		}

		return new List<SelectListItem>();
	}

	private async Task<ApiResponse<PartBranchDTO>> PartBranchServiceResponseAsync(int partBranchId)
	{
		ApiResponse<PartBranchDTO> response = new();
		var apiResponse = await _apiProvider.GetAsync($"projects/parts/branches/{partBranchId}");
		if (apiResponse.IsSuccessStatusCode)
		{
			response = await apiResponse.Content.ReadAsAsync<ApiResponse<PartBranchDTO>>();
			if (!response.IsSuccess)
			{
				// response..SetError(response.Message, 500);
			}
		}
		else
		{
			// response.SetError("REST API error", response.HttpCode);
		}

		return response;
	}
}

