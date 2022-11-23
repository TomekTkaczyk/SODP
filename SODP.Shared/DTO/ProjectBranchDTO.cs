using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class ProjectBranchDTO
    {
        public int Id { get; set; }
        public BranchDTO Branch { get; set; }

        public ICollection<ProjectBranchRoleDTO> Roles { get; set; }

    }
}
