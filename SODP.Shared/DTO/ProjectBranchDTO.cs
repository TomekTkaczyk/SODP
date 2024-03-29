﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class ProjectBranchDTO
    {
        public int Id { get; set; }
        public PartDTO Part { get; set; }

        public ICollection<BranchRoleDTO> Roles { get; set; }

    }
}
