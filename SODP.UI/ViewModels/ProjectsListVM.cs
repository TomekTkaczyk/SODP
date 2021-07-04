using SODP.Domain.Models;
using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class ProjectsListVM
    {
        public IList<ProjectDTO> Projects { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
