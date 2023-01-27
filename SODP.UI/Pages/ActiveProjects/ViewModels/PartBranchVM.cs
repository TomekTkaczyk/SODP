using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
	public class PartBranchVM
	{
		public int Id { get; set; }

		public BranchVM Branch { get; set; }

		public ICollection<RoleVM> Roles { get; set; }
	}
}
