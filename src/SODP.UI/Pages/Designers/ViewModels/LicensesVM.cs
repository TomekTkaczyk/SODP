using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels;

public class LicensesVM
{
    public int DesignerId { get; set; }
    
    public ICollection<LicenseVM> Licenses { get; set; }

}
