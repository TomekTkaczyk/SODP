using SODP.Application.ValueObjects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Investors.ViewModels
{
	public sealed class InvestorsVM
	{
		public List<InvestorValueObject> Investors { get; set; }

        public PageInfo PageInfo { get; set; }

    }
}
