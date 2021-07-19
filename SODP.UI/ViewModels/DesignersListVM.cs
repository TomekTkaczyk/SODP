using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class DesignersListVM
    {
        public IList<DesignerDTO> Designers { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
