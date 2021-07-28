using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class LicencesListVM
    {
        public int DesignerId { get; set; }
        public IList<LicenceDTO> Licences { get; set; }
    }
}
