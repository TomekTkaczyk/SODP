using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class LicenseDTO : BaseDTO
    {
        public DesignerDTO Designer { get; set; }
        public string Content { get; set; }
    }
}
