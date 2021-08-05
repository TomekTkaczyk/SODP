using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class LicenseWithBranchesDTO : LicenseDTO
    {
        public IList<BranchDTO> Branches { get; set; }
    }
}
