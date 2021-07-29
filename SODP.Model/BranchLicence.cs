using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class BranchLicence : BaseEntity
    {
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public int LicenceId { get; set; }
        public virtual Licence Licence { get; set; }
    }
}
