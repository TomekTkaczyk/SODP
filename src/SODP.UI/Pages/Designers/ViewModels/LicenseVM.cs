using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class LicenseVM  : NewLicenseVM
    {
        public int Id { get; set; }

        public SelectListItem Branch { get; set; }

        public int BranchId { get; set; }

        public List<SelectListItem> Branches { get; set; }

        public List<SelectListItem> ApplyBranches { get; set; }
    }
}
