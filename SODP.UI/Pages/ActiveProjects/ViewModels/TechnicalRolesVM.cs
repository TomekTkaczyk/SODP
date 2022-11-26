using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.Enums;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class TechnicalRolesVM
    {
        public int ProjectId { get; set; }

        public int BranchId { get; set; }

        public IDictionary<TechnicalRole,LicenseVM> Roles { get; set; }

        public int LicenseId { get; set; }

        public ICollection<SelectListItem> Licenses { get; set; }

        public ICollection<SelectListItem> AvailableRoles { get; set; }
    }
}
