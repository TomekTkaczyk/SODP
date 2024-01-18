using SODP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO;

public class ProjectDTO : BaseDTO
{
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

	public ProjectStatus Status { get; set; }

	public ICollection<ProjectPartDTO> Parts { get; set; }
}
