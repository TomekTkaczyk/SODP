using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
	public class InvestorsVM
	{
		public int ProjectId { get; set; }

		public List<SelectListItem> Investors { get; set; }

		public SelectListItem Selected { get; set; }

		public int? InvestorId { get; set; }


	}
}
