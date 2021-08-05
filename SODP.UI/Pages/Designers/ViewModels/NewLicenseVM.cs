using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class NewLicenseVM
    {
        public int DesignerId { get; set; }
    
        public string Contents { get; set; }

        public IList<BranchDTO> Branches { get; set; }
    }
}
