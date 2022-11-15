using SODP.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Model
{
    public class ProjectBranchRole
    {
        public int Id { get; set; }

        public int ProjectBranchId { get; set; }

        public virtual ProjectBranch ProjectBranch { get; set; }

        public TechnicalRole Role { get; set; }

        public int LicenseId { get; set; }

        public virtual License License { get; set; }

    }
}
