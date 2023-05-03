using SODP.Shared.DTO;
using SODP.UI.Api;
using System.Collections.Generic;

namespace SODP.UI.Pages.Shared.ViewModels
{
    public class ProjectsVM
    {
        public ICollection<ProjectDTO> Projects { get; set; }

        public PageInfo PageInfo { get; set; }

    }
}
