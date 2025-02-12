﻿using System.Collections.Generic;

namespace SODP.UI.Pages.Shared.ViewModels
{
    public class ProjectPartVM
    {
        public int Id { get; set; }

        public string Sign { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public ICollection<PartBranchVM> Branches { get; set; }
    }
}
