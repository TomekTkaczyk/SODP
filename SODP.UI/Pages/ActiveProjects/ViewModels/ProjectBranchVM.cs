using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class ProjectBranchVM
    {

        public int Id { get; set; }

        public BranchVM Branch { get; set; }

        public ICollection<RoleVM> Roles { get; set; }

    }
}
