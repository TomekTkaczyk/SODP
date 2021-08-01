using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class DesignersVM
    {
        public IList<DesignerDTO> Designers { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
