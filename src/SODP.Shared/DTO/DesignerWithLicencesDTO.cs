using System.Collections.Generic;

namespace SODP.Shared.DTO;

public class DesignerWithLicensesDTO : DesignerDTO
{
	public IList<LicenseDTO> Licenses { get; set; }
}
