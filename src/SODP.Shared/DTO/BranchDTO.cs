using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record BranchDTO(
	int Id,
	string Sign,
	string Title,
	int Order,
	bool ActiveStatus,
	IEnumerable<LicenseDTO> Licenses);