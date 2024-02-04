using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Domain.Entities;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared.Extensions;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects;

[Authorize(Roles = "Administrator,ProjectManager")]
public class EditModel : AppPageModel
{
	const string _editProjectPartViewName = "ModalView/_EditProjectPartModalView";
	const string _addTechnicalRoleViewName = "ModalView/_AddTechnicalRoleModalView";
	const string _addPartBranchViewName = "ModalView/_AddPartBranchModalView";
	const string _getInvestorViewName = "ModalView/_GetInvestorModalView";

	const string _projectPartDetailsViewName = "PartialView/_ProjectPartDetailsPartialView";

	public EditModel(
		IWebAPIProvider apiProvider,
		ILogger<EditModel> logger,
		LanguageTranslatorFactory translatorFactory) 
		: base(apiProvider, logger, translatorFactory) 
	{
		_endpoint = "projects";
	}


	public ProjectDetailsVM Project { get; set; }

	//public InvestorsVM Investors { get; set; }

	//public TechnicalRoleVM TechnicalRoles { get; set; }

	public async Task<IActionResult> OnGetAsync(int id)
	{
		var apiResponse = await GetApiResponseAsync<ProjectDTO>($"{_endpoint}/{id}/details");
		if (!apiResponse.IsSuccess) 
		{
			return Redirect("/Errors/404");
		}
		Project = apiResponse.Value.ToProjectDetailsVM();

		return Page();
	}

	public async Task<IActionResult> OnPostAsync(ProjectDetailsVM project)
	{
		if (ModelState.IsValid)
		{
			var content = project.ToHttpContent();
			var apiResponse = await _apiProvider.PutAsync(
				$"projects/{project.Id}",
				content);

			if (apiResponse.IsSuccessStatusCode)
			{
				return RedirectToPage("Index");
			}

			var response = await _apiProvider.GetContentAsync<ApiResponse>(apiResponse);
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
		var model = new ProjectPartEditVM()
		{
			Id = projectPartId,
			ProjectId = projectId,
			Items = await GetAvailablePartsAsync(),
		};

		if( projectPartId != 0 )
		{
			var apiResponse = await GetApiResponseAsync<ProjectPartDTO>($"projects/parts/{projectPartId}");

			if (apiResponse.Value is not null)
			{
				model.Sign = apiResponse.Value.Sign;
				model.Title = apiResponse.Value.Title;
			}
		}

		return GetPartialView(model, _editProjectPartViewName);
	}

	public async Task<IActionResult> OnPostEditProjectPartAsync(ProjectPartEditVM part)
	{
		if (ModelState.IsValid)
		{
			var responseMessage = part.Id == 0
				? await _apiProvider.PostAsync($"projects/{part.ProjectId}/parts", part.ToHttpContent())
				: await _apiProvider.PutAsync($"projects/parts/{part.Id}", part.ToHttpContent());

			if (!responseMessage.IsSuccessStatusCode)
			{
				var message = await responseMessage.Content.ReadAsAsync<ApiResponse>();
				SetModelErrors(message);

			}
		}
				
		part.Items = await GetAvailablePartsAsync();

		return GetPartialView(part, _editProjectPartViewName);
	}

	public async Task<PartialViewResult> OnGetPartDetailsPartialAsync(int projectPartId)
	{
		var apiResponse = await GetApiResponseAsync<ProjectPartDTO>($"projects/parts/{projectPartId}/details");

        var model = apiResponse.Value.ToProjectPartDetailsVM();

		//projectPartDetails.BranchesToSelect = new AvailableBranchesVM()
		//{
		//	ProjectPartId = projectPartId,
		//	Items = apiResponse.Value.AvailableBranches.Select(x => new SelectListItem
		//	{
		//		Value = x.Id.ToString(),
		//		Text = x.ToString()
		//	}).ToList()
		//};

		var partialView = GetPartialView(model, _projectPartDetailsViewName);

		partialView.ViewData.Add("ProjectPartId", projectPartId);

		return partialView;
	}

	public async Task<PartialViewResult> OnGetAddPartBranchAsync(int projectPartId)
	{
		var model = new AvailableBranchesVM
		{
			ProjectPartId = projectPartId
		};

		var branchesApiResponse = await GetApiResponseAsync<Page<BranchDTO>>($"Branches?ActiveStatus=true");
		if (branchesApiResponse.IsSuccess)
		{
			model.Items = branchesApiResponse.Value.Collection.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = $"{x.Sign.Trim()}-{x.Title.Trim()}"
			}).ToList();
		}

		var partApiResponse = await GetApiResponseAsync<ProjectPartDTO>($"projects/parts/{projectPartId}/details");
		if (partApiResponse.IsSuccess)
		{
			foreach (var item in partApiResponse.Value.Branches)
			{
				var selected = model.Items.SingleOrDefault(x => x.Value == item.Branch.Id.ToString());
				model.Items.Remove(selected);
			}
		}

		return GetPartialView(model, _addPartBranchViewName);
	}

	public async Task<PartialViewResult> OnPostAddPartBranchAsync(AvailableBranchesVM model)
	{
		model.Items = new List<SelectListItem>();
		if (ModelState.IsValid)
		{
			if (model.SelectedId.HasValue && (model.SelectedId != 0))
			{
				var apiResponse = await _apiProvider.PostAsync($"projects/parts/{model.ProjectPartId}/branches/{model.SelectedId}", new StringContent(""));
				if (apiResponse.StatusCode == HttpStatusCode.OK)
				{
					var response = await _apiProvider.GetContentAsync<ApiResponse>(apiResponse);
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
		var apiResponse = await GetApiResponseAsync<PartBranchDTO>($"projects/parts/branches/{partBranchId}");
		var roles = GetAvailableRoles(apiResponse.Value);
		var designers = await GetAvailableDesignersAsync(apiResponse.Value);

		var model = new AvailableRolesVM
		{
			PartBranchId = partBranchId,
			ItemsRole = roles,
			ItemsLicense = designers,
		};

		return GetPartialView(model, _addTechnicalRoleViewName);
	}

	public async Task<PartialViewResult> OnPostAddTechnicalRoleAsync(AvailableRolesVM model)
	{
		var partBranchResponse = await GetApiResponseAsync<PartBranchDTO>($"projects/parts/branches/{model.PartBranchId}");
		model.ItemsRole = GetAvailableRoles(partBranchResponse.Value);
		model.ItemsLicense = await GetAvailableDesignersAsync(partBranchResponse.Value);
		if (ModelState.IsValid)
		{
			var role = new NewPartBranchRoleVM
			{
				partBranchId = model.PartBranchId,
				Role = (TechnicalRole)Enum.Parse(typeof(TechnicalRole), model.SelectedRoleId.ToString()),
				LicenseId = (int)model.SelectedLicenseId
			};
			var apiResponse = await _apiProvider.PostAsync($"projects/parts/branches/roles", role.ToHttpContent());
			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				var response = await _apiProvider.GetContentAsync<ApiResponse>(apiResponse);
				if (!response.IsSuccess)
				{
					// SetModelErrors(response);
				}
			}
		}

		return GetPartialView(model, _addTechnicalRoleViewName);
	}

	#region private_methods

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
		var apiResponse = await GetApiResponseAsync<BranchDTO>($"branches/{partBranch.Branch.Id}/details");
		var licenses = apiResponse.Value.Licenses.ToList();
		licenses.RemoveAll(x => partBranch.Roles.Select(x => x.License).Any());
		var designers = licenses.Select(x => new SelectListItem
		{
			Value = x.Id.ToString(),
			Text = $"{x.Designer.Title} {x.Designer.Firstname} {x.Designer.Lastname} ({x.Content})"
		}).ToList();

		return new SelectList(designers, "Value", "Text");
	}

	private async Task<IList<SelectListItem>> GetAvailablePartsAsync()
	{
		var apiResponse = await GetApiResponseAsync<Page<ProjectPartDTO>>($"parts?active=true");
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

	#endregion
}

