using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Application.Mappers;

public static class PartBranchMapper
{
    public static PartBranchDTO ToDTO(this PartBranch partBranch, List<object> mapCache = null)
    {
        if (partBranch == null) return null;

        mapCache ??= new List<object>();

        if(mapCache.Contains(partBranch)) {
            return new PartBranchDTO(
                partBranch.Id,
                default,
                default);
        }

        var partBranchDTO = new PartBranchDTO(
            partBranch.Id,
            partBranch.Branch.ToDTO(),
            (partBranch.Roles is not null)
                ? partBranch.Roles.Select(x => x.ToDTO())
                : default);

        mapCache.Add(partBranch);

        return partBranchDTO;
    }
}
