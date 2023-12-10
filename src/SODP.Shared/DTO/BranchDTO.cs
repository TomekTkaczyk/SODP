using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record BranchDTO
{
	public int Id { get; set; }

	public string Sign { get; set; }

	public string Title { get; set; }

	public int Order { get; set; }

	public bool ActiveStatus { get; set; }

	public IReadOnlyCollection<LicenseDTO> Licenses { get; set; }
}