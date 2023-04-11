using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record DesignerWithLicensesDTO : DesignerDTO
{
	public IList<LicenseDTO> Licenses { get; set; }
}
