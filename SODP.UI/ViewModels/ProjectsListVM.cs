using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class ProjectsListVM
    {                           
        public string SearchString { get; set; }
        public IList<ProjectDTO> Projects { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
