using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class DesignerWithLicensesDTO : DesignerDTO
    {
        public IList<LicenseDTO> Licenses { get; set; }
    }
}
