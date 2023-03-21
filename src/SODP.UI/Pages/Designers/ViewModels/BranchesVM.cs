using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class BranchesVM
    {
        public int LicenseId { get; set; }

        public int BranchId { get; set; }

        public List<SelectListItem> Branches { get; set; }
    }
}
