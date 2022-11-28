using SODP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class TechnicalRoleDTO
    {
        public int ProjectId { get; set; }

        public int BranchId { get; set; }

        public TechnicalRole Role { get; set; }

        public int LicenseId { get; set; }

    }
}
