using System.Collections;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels;

public class ProjectPartDetailsVM
{
	public int Id { get; set; }

	public string Sign { get; set; }

	public string Title { get; set; }

	public int Order { get; set; }

	public ICollection<PartBranchVM> Branches { get; set; }

	public ICollection<BranchVM> AvailableBranches { get; set; }

	public AvailableBranchesVM BranchesToSelect { get; set; }
}
