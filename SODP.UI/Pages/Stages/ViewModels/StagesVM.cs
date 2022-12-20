using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Stages.ViewModels
{
    public class StagesVM
    {
        public string SearchString { get; set; }
        public List<StageVM> Stages { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
