using SODP.Shared.DTO;
using SODP.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
	public class ProjectDetailVM
	{
		public int Id { get; set; }

		public string Number { get; set; }

		public StageDTO Stage { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }

		public string Address { get; set; }

		public string LocationUnit { get; set; }

		public string BuildingCategory { get; set; }

		public string Investor { get; set; }

		public string BuildingPermit { get; set; }

		public string Description { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime? DevelopmentDate { get; set; }

		public ICollection<ProjectPartDTO> Parts { get; set; }
	}
}
