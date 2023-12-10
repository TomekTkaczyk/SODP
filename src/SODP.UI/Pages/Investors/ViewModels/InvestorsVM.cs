using SODP.UI.Api;
using System.Collections.Generic;

namespace SODP.UI.Pages.Investors.ViewModels;

public class InvestorsVM
	{
		public ICollection<InvestorVM> Investors { get; set; }

    public PageInfo PageInfo { get; set; }

}
