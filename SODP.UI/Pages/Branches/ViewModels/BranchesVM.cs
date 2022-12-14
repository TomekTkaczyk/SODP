using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Branches.ViewModels
{
    public class BranchesVM
    {
        public IList<BranchDTO> Branches { get; set; }
        public PageInfo PageInfo { get; set; }

    }
}
