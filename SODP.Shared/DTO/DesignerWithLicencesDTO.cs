using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class DesignerWithLicencesDTO : DesignerDTO
    {
        public IList<LicenceDTO> Licences { get; set; }
    }
}
