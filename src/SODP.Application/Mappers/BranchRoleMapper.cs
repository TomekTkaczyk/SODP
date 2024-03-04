using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.Application.Mappers;

public static class BranchRoleMapper
{
    public static BranchRoleDTO ToDTO(this BranchRole branchRole, List<object> mapCache = null)
    {
        if(branchRole == null) {
            return null;
        }

        mapCache ??= new List<object>();

        if(mapCache.Contains(branchRole)) {
            return new BranchRoleDTO(
                branchRole.Id,
                branchRole.Role.ToString(),
                default);
        }

        mapCache.Add(branchRole);

        return new BranchRoleDTO(
            branchRole.Id,
            branchRole.Role.ToString(),
            branchRole.License.ToDTO());
    }
}
