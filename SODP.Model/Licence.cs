using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class Licence : BaseEntity
    {
        public int DesignerId { get; set; }
        public virtual Designer Designer { get; set; }
        public virtual ICollection<BranchLicence> Branches { get; set; }
        public string Contents { get; set; }
    }
}
