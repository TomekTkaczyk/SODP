using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class GetDesignerVM
    {
        public int? DesignerId { get; set; }

        public IEnumerable<SelectListItem> Designers { get; set; }
    }
}
