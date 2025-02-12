﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class AvailableBranchesVM
    {
        public int ProjectPartId { get; set; }

        public int? SelectedId { get; set; }

        public IList<SelectListItem> Items { get; set; }

    }
}
