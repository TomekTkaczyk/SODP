using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels;

public class BranchesVM
{
	public int LicenseId { get; set; }

	public int BranchId { get; set; }

	public List<SelectListItem> Branches { get; set; }
}
