using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record LicenseDTO
{
	public int Id { get; set; }

	public DesignerDTO Designer { get; set; }

	public string Content { get; set; }

	public ICollection<BranchDTO> Branches { get; set; }
}
