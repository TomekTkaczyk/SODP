using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class BranchLicense : BaseEntity
    {
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public int LicenseId { get; set; }
        public virtual License License { get; set; }
    }
}
