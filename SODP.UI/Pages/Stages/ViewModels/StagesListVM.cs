using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Stages.ViewModels
{
    public class StagesListVM
    {
        public string SearchString { get; set; }
        public IList<StageDTO> Stages { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
