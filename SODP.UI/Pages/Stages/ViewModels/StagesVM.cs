using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Stages.ViewModels
{
	public class StagesVM
	{
		public IList<StageDTO> Stages { get; set; }

		public PageInfo PageInfo { get; set; }
	}
}
