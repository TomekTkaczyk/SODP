using SODP.Domain.Models;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class StagesListVM
    {
        public IList<StageDTO> Stages { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
