using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class AvailableRolesVM
    {
		[Required]
        public int PartBranchId { get; set; }

		[Required]
        public int? SelectedRoleId { get; set; }

		public SelectList ItemsRole { get; set; }

		[Required]
		public int? SelectedLicenseId { get; set; }

		public SelectList ItemsLicense { get; set; }
	}
}