using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class SetDesignerVM
    {
        public int ProjectId { get; set; }

        public int BranchId { get; set; }

        public int Selector { get; set; }

        public int? DesignerId { get; set; }

        public IEnumerable<SelectListItem> Designers { get; set; }
    }
}
