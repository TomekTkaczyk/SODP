using SODP.Shared.DTO;
using SODP.UI.Api;
using System.Collections.Generic;

namespace SODP.UI.Pages.Stages.ViewModels;

public class StagesVM
{
	public ICollection<StageDTO> Stages { get; set; }

	public PageInfo PageInfo { get; set; }
}
