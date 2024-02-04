using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;

namespace SODP.Application.Mappers;

public static class BranchRoleMapper
{
    public static BranchRoleDTO ToDTO(this BranchRole branchRole)
    {
        if (branchRole == null) throw new ArgumentNullException(nameof(branchRole));

        return new BranchRoleDTO(
            branchRole.Id,
            branchRole.Role.ToString(),
            branchRole.License.ToDTO());
    }
}
