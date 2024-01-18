using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record DesignerLicensesDTO(
	DesignerDTO Designer,
	ICollection<LicenseDTO> Licenses)
{ }
