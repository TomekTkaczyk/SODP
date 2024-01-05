using System.Collections;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels;

public record BranchDetailsVM
{
	public int Id { get; set; }

	public string Sign { get; set; }

	public string Title { get; set; }

	public int Order { get; set; }

	public bool ActiveStatus { get; set; }

	public ICollection<LicenseVM> Licenses { get; set; }
	
	public override string ToString()
	{
		return Sign.Trim() + "-" + Title.Trim();
	}
}
