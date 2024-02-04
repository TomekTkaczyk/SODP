using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Application.Mappers;

public static class PartBranchMapper
{
    public static PartBranchDTO ToDTO(this PartBranch partBranch)
    {
        if (partBranch == null) throw new ArgumentNullException(nameof(partBranch));

        return new PartBranchDTO(
            partBranch.Id,
            partBranch.Branch.ToDTO(),
            (partBranch.Roles is null)
                ? partBranch.Roles.Select(x => x.ToDTO())
                : new List<BranchRoleDTO>());
    }
}
