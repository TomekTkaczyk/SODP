using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class LicensesVM
    {
        public int DesignerId { get; set; }
        public IList<LicenseWithBranchesDTO> Licenses { get; set; }
    }
}
