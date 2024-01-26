using DocumentFormat.OpenXml.Wordprocessing;
using SODP.Domain.ValueObjects;
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

	public static ProjectPartDetailsVM ToProjectPartDetailsVM(this ProjectPartDTO projectPart)
	{
		ProjectPartDetailsVM result = new()
		{
			Id = projectPart.Id,
			Order = projectPart.Order,
			Sign = projectPart.Sign,
			Title = projectPart.Title,
			Branches = projectPart.Branches.Select(x => x.ToPartBranchVM()).ToList(),
		};

		return result;
	}

	public static BranchVM ToBranchVM(this BranchDTO branch)
	{
		BranchVM result = new()
		{
			Id = branch.Id,
			Sign = branch.Sign,
			Title = branch.Title,
			ActiveStatus = branch.ActiveStatus
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
			Designer = branchRole.License.Designer.ToDesignerVM().Name,
			Content = branchRole.License.Content,
			License = branchRole.License.ToLicenseVM()
		};

		return result;
	}

	public static LicenseVM ToLicenseVM(this LicenseDTO license)
	{
		var branches = license.Branches.Select(x => x.ToBranchVM());

		LicenseVM result = new(
			license.Id,
			license.Designer.ToString(),
			license.Content,
			branches
		);

		return result;
	}

	public static PartVM ToPartVM(this PartDTO part)
	{
		return new PartVM()
		{
			Id = part.Id,
			Sign = part.Sign,
			Title = part.Title,
			ActiveStatus = part.ActiveStatus
		};
	}

	public static InvestorVM ToInvestorVM(this InvestorDTO investor)
	{
		return new InvestorVM()
		{
			Id = investor.Id,
			Name = investor.Name,
			ActiveStatus = investor.ActiveStatus
		};
	}

	public static UserVM ToUserVM(this UserDTO user)
	{
		return new UserVM()
		{
			Id = user.Id,
			Firstname = user.Firstname,
			Lastname = user.Lastname,
			Username = user.Username,
			ActiveStatus = user.ActiveStatus,
			Roles = user.Roles
		};
	}

	public static DesignerVM ToDesignerVM(this DesignerDTO designer)
	{
		var licenses = designer.Licenses.Select(x => x.ToLicenseVM());

        DesignerVM result = new(
			designer.Id,
			designer.Title + " " + designer.Firstname + " " +designer.Lastname,
			designer.ActiveStatus,
			licenses
			);

		return result;
	}
}
