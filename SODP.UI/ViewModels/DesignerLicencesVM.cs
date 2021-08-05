using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.ViewModels
{
    public class DesignerLicensesVM
    {
        public int DesignerId { get; set; }

        public IList<LicenseDTO> License { get; set; }
    }
}
