using SODP.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class BranchLicense : BaseEntity
    {
        public TechnicalRole Role { get; set; }
        public int LicenseId { get; set; }
        public virtual License License { get; set; }
    }
}
