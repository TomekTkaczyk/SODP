using SODP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class TechnicalRoleDTO : BaseDTO
    {
        public BranchDTO Branch { get; set; }

        public TechnicalRole Role { get; set; }

        public LicenseDTO License { get; set; }

    }
}
