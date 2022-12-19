using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Investors.ViewModel
{
	public class InvestorsVM
	{
		public IList<InvestorDTO> Investors { get; set; }
		public PageInfo PageInfo { get; set; }
	}
}
