using SODP.Shared.DTO;
using SODP.UI.Pages.Shared.ViewModels;

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
			Title = project.Title
			// Parts = project.Parts,
		};

		return result;
	}
}
