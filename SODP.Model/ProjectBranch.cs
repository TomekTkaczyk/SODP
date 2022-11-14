using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class ProjectBranch
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public int? DesignerLicenseId { get; set; }
        public virtual License DesignerLicense { get; set; }
        public int? CheckingLicenseId { get; set; }
        public virtual License CheckingLicense { get; set; }
        public ICollection<BranchLicense> Roles { get; set; }

        public override string ToString()
        {
            return Branch.ToString();
        }
    }
}
