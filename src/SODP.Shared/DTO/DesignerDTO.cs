using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record DesignerDTO(
	int Id,
	string Title,
	string Firstname,
	string Lastname,
	bool ActiveStatus,
	IList<LicenseDTO> Licenses)
{
	public override string ToString()
	{
		return $"{(Title ?? string.Empty).Trim()} {(Firstname ?? string.Empty).Trim()} {(Lastname ?? string.Empty).Trim()}";
	}
}
