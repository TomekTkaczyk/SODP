using System.Collections.Generic;

namespace SODP.UI.Pages.Shared.ViewModels
{
    public class PartBranchVM
    {
        public int Id { get; set; }

        public BranchVM Branch { get; set; }

        public ICollection<BranchRoleVM> Roles { get; set; }
    }
}
