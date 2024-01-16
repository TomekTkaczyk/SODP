using SODP.Shared.DTO;
using SODP.UI.Pages.Shared.ViewModels;
using System.Linq;

namespace SODP.UI.Pages.Shared.Extensions;

public static class DTOExtensions
{
	public static ProjectVM ToProjectVM(this ProjectDTO project)
	{
		return new ProjectVM
		{
			Id = project.Id,
			Name = project.Name,
			Number = project.Number,
			Stage = project.Stage.ToStageVM()
		};
	}

	public static StageVM ToStageVM(this StageDTO stage)
	{
		return new StageVM
		{
			Id = stage.Id,
			Sign = stage.Sign,
			Title = stage.Title,
			ActiveStatus = stage.ActiveStatus
		};
	}

	public static ProjectDetailsVM ToProjectDetailsVM(this ProjectDTO project)
	{
		ProjectDetailsVM result = new()
		{
			Id = project.Id,
			Name = project.Name,
			Address = project.Address,
			BuildingCategory = project.BuildingCategory,
			BuildingPermit = project.BuildingPermit,
			Description = project.Description,
			DevelopmentDate = project.DevelopmentDate,
			Investor = project.Investor,
			LocationUnit = project.LocationUnit,
			Number = project.Number,
			Stage = project.Stage.ToStageVM(),
			Title = project.Title,
			Parts = project.Parts.Select(x => x.ToProjectPartVM()).ToList()
		};

		return result;
	}

	public static ProjectPartVM ToProjectPartVM(this ProjectPartDTO projectPart)
	{
		ProjectPartVM result = new()
		{
			Id = projectPart.Id,
			Order = projectPart.Order,
			Sign = projectPart.Sign,
			Title = projectPart.Title,
			Branches = projectPart.Branches.Select(x => x.ToPartBranchVM()).ToList()	
		};

		return result;
	}

	public static BranchVM ToBranchVM(this BranchDTO branch)
	{
		BranchVM result = new()
		{
			Id = branch.Id,
			Sign = branch.Sign,
			Title = branch.Title
		};

		return result;
	}

	public static PartBranchVM ToPartBranchVM(this PartBranchDTO partBranch)
	{
		PartBranchVM result = new()
		{
			Id = partBranch.Id,
			Branch = partBranch.Branch.ToBranchVM(),
			Roles = partBranch.Roles.Select(x => x.ToBranchRoleVM()).ToList()
		};

		return result;
	}

	public static BranchRoleVM ToBranchRoleVM(this BranchRoleDTO branchRole)
	{
		BranchRoleVM result = new()
		{
			Id = branchRole.Id,
			Role = branchRole.Role,
			Designer = branchRole.License.Designer.ToString(),
			Content = branchRole.License.Content,
			License = branchRole.License.ToLicenseVM()
		};

		return result;
	}

	public static LicenseVM ToLicenseVM(this LicenseDTO license)
	{
		LicenseVM result = new()
		{
			Id = license.Id,
			Content = license.Content,
			Designer = license.Designer.ToString(),
		};

		return result;
	}
}
