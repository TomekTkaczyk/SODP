using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class NewProjectVM
    {
        public ProjectDTO Project { get; set; }
        public IEnumerable<SelectListItem> Stages { get; set; }
    }
}
