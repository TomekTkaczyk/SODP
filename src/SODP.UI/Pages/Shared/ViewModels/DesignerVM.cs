using System.Collections.Generic;

namespace SODP.UI.Pages.Shared.ViewModels;

public record DesignerVM(
    int Id,
    string Name,
    bool ActiveStatus,
    IEnumerable<LicenseVM> Licenses 
    )
{ }
