using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record BranchDTO : NewBranchDTO
{
	public int Order { get; set; }

	public bool ActiveStatus { get; set; }

	public IReadOnlyCollection<LicenseDTO> Licenses { get; set; }
}