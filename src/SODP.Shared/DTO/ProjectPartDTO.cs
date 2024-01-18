using System.Collections.Generic;

namespace SODP.Shared.DTO;

public class ProjectPartDTO
{
	public int Id { get; set; }

	public string Sign { get; set; }

	public string Title { get; set; }

	public int Order { get; set; }

	public ICollection<PartBranchDTO> Branches { get; set; }

	public ICollection<BranchDTO> AvailableBranches { get; set; }
}
