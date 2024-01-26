using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record LicenseDTO(
	int Id,
	DesignerDTO Designer,
	string Content,
	IEnumerable<BranchDTO> Branches);
