using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
	public class PartBranchesVM
	{
		public ProjectPartVM  ProjectPart { get; set; }

		public IList<PartBranchVM> Branches { get; set; }
	}
}
