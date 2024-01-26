using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.UI.Pages.Shared.ViewModels;
using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class LicenseVM
    {
        public int Id { get; set; }
		
        public DesignerVM Designer { get; set; }

		public string Content { get; set; }

        public ICollection<BranchVM> Branches { get; set; }
    }
}
