using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class BranchDesignersVM
    {
        public int BranchId { get; set; }

        public IList<DesignerDTO> Designers { get; set; }
    }
}
