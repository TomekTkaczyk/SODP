using SODP.Shared.DTO;

namespace SODP.UI.Pages.ActiveProjects.ViewModels;

public class ProjectVM
{
	public int Id { get; set; }

	public string Number { get; set; }

	public StageDTO Stage { get; set; }

	public string Name { get; set; }
}
