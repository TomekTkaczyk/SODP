using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Application.Mappers;

public static class ProjectPartMapper
{
    public static ProjectPartDTO ToDTO(this ProjectPart projectPart)
    {
        return projectPart.ToDTO(new List<Branch>());
    }

    public static ProjectPartDTO ToDTO(this ProjectPart projectPart, IEnumerable<Branch> availableBranches)
    {
        if (projectPart == null) throw new ArgumentNullException(nameof(projectPart));

        return new ProjectPartDTO(
            projectPart.Id,
            projectPart.Sign,
            projectPart.Title,
            projectPart.Order,
            (projectPart.Branches is not null) 
                ? projectPart.Branches.Select(x => x.ToDTO()) 
                : new List<PartBranchDTO>(),
            availableBranches.Select(x => x.ToDTO()));
    }
}
