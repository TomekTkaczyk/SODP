using System.Collections.Generic;

namespace SODP.Shared.DTO;

public record PartBranchDTO(
    int Id,
    BranchDTO Branch,
    IEnumerable<BranchRoleDTO> Roles);
