using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record DesignerLicensesDTO(
	DesignerDTO designer, 
	ICollection<LicenseDTO> licenses)
{
}
