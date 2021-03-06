using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class ProjectsListVM
    {                           
        public IList<ProjectDTO> Projects { get; set; }
    }
}
