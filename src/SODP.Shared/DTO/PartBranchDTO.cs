using System.Collections.Generic;

namespace SODP.Shared.DTO;

public class PartBranchDTO : BaseDTO
{
	public BranchDTO Branch { get; set; }

	public ICollection<BranchRoleDTO> Roles { get; set; }

}
