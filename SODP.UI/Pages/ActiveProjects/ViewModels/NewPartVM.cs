using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
	public class NewPartVM
	{
		public int ProjectId { get; set; }
		public string Sign { get; set; }
		public string Name { get; set; }
        public IList<SelectListItem> Items { get; set; }
    }
}
