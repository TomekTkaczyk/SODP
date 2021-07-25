using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class BranchDesigner
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public int DesignerId { get; set; }
        public virtual Designer Designer { get; set; }
    }
}
