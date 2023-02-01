using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class PartBranchDTO : BaseDTO
    {
        public int ProjectPartId { get; set; }

        //public PartDTO Part { get; set; }

        public BranchDTO Branch  { get; set; }

        public ICollection<BranchRoleDTO> Roles { get; set; }

    }
}
