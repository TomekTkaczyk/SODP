using System.Collections.Generic;
using SODP.UI.Pages.Shared.ViewModels;

namespace SODP.UI.Pages.Designers.ViewModels;

public class DesignerLicensesVM
{
	public DesignerVM Designer { get; set; }
	public ICollection<LicenseVM> Licenses { get; set; }
}
