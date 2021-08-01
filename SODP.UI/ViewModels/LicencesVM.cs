using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class LicencesVM
    {
        public int DesignerId { get; set; }
        public IList<LicenceWithBranchesDTO> Licences { get; set; }
    }
}
