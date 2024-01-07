using SODP.Shared.DTO;
using SODP.UI.Pages.Shared.PageModels;

namespace SODP.UI.Extensions;

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
}
