using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Investors.ViewModels
{
	public class InvestorsVM
	{
		public ICollection<InvestorDTO> Investors { get; set; }

        public PageInfo PageInfo { get; set; }

    }
}
