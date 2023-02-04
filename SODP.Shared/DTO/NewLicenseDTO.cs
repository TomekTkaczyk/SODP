using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class LicenseDTO : BaseDTO
    {
        public DesignerDTO Designer { get; set; }
        public string Content { get; set; }

        public IList<BranchDTO> Branches { get; set; }
    }
}
