using SODP.Domain.DTO;
using SODP.Domain.Models;
using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class StagesListVM
    {
        public IList<StageDTO> Stages { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
