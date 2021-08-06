using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class LicensesVM
    {
        public int DesignerId { get; set; }
        public IList<LicenseWithBranchesDTO> Licenses { get; set; }
    }
}
