using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class License : BaseEntity
    {
        public int DesignerId { get; set; }
        public virtual Designer Designer { get; set; }
        public string Content { get; set; }
        public virtual ICollection<LicenseBranch> Branches { get; set; }
    }
}
