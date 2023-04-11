using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record LicenseDTO // : BaseDTO
{
	public int Id { get; set; }

	public DesignerDTO Designer { get; set; }

	public string Content { get; set; }

	public IList<BranchDTO> Branches { get; set; }
}
