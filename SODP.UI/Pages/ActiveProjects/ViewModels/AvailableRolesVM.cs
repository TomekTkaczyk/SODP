using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class AvailableRolesVM
    {
        public int PartBranchId { get; set; }

        public int? SelectedRoleId { get; set; }

		public IList<SelectListItem> ItemsRole { get; set; }

		public int? SelectedLicenseId { get; set; }

		public IList<SelectListItem> ItemsLicense { get; set; }
	}
}