using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ArchiveProjects.ViewModels
{
    public class AvailableBranchesVM
    {
        public int? SelectedId { get; set; }

        public IList<SelectListItem> Items { get; set; }

    }
}
