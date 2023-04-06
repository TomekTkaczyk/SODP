using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record BranchWithLicensesDTO : BranchDTO
{
	public IList<LicenseDTO> Licenses { get; set; }
}
