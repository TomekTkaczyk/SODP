using System.Collections.Generic;

namespace SODP.UI.Pages.Shared.ViewModels;

public record LicenseVM(
    int Id,
    string Designer,
    string Content,
    IEnumerable<BranchVM> Branches);
