using SODP.Domain.DTO;
using SODP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.ViewModels
{
    public class ProjectsListVM
    {
        public IList<ProjectDTO> Projects { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
