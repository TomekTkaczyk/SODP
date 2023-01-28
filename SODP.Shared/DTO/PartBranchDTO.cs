using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class PartBranchDTO : BaseDTO
    {
        public PartDTO Part { get; set; }

        public ICollection<BranchRoleDTO> Roles { get; set; }

    }
}
