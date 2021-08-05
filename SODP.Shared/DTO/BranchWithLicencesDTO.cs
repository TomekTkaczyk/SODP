using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class BranchWithLicensesDTO : BranchDTO
    {
        public IList<LicenseDTO> Licenses { get; set; }
    }
}
