﻿using SODP.UI.Pages.Shared.ViewModels;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class PartBranchVM
    {
        public int Id { get; set; }

        public BranchVM Branch { get; set; }

        public ICollection<BranchRoleVM> Roles { get; set; }
    }
}