using SODP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SODP.Shared.DTO
{
    public class ProjectBranchRoleDTO : BaseDTO
    {
        public int Id { get; set; }

        public int ProjectBranchId { get; set; }

        public string Role { get; set; }

        public virtual LicenseDTO License { get; set; }
    }
}
