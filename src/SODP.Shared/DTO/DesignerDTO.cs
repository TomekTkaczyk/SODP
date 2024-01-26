using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record DesignerDTO(
	int Id,
	string Title,
	string Firstname,
	string Lastname,
	bool ActiveStatus,
	IEnumerable<LicenseDTO> Licenses);
