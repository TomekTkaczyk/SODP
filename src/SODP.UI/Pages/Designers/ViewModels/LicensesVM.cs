using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels;

public class LicensesVM
{
    public int DesignerId { get; set; }
    
    public List<LicenseVM> Licenses { get; set; }

}
