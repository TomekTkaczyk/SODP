using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class LicenceDTO : BaseDTO
    {
        public DesignerDTO Designer { get; set; }
        public string Contents { get; set; }
    }
}
