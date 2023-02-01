using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
	public class ProjectPartVM
	{
		public int Id { get; set; }

		public string Sign { get; set; }

		public string Name { get; set; }

		public ICollection<BranchVM> Branches { get; set; }
	}
}
