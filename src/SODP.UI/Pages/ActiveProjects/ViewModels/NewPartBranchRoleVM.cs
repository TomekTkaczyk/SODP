using SODP.Shared.Enums;

namespace SODP.UI.Pages.ActiveProjects.ViewModels;

public class NewPartBranchRoleVM
{
    public int partBranchId { get; set; }

    public TechnicalRole Role { get; set; }

    public int LicenseId { get; set; }
}
