using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels;

public class DesignerLicensesVM
{
	public DesignerVM Designer { get; set; }
	public ICollection<LicenseVM> Licenses { get; set; }
}
