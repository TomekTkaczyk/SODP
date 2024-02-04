using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record ProjectPartDTO(
	int Id,
	string Sign,
	string Title,
	int Order,
	IEnumerable<PartBranchDTO> Branches,
	IEnumerable<BranchDTO> AvailableBranches);
