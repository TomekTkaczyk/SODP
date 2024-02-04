using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Application.Mappers;

public static class LicenseMapper
{
    public static LicenseDTO ToDTO(this License license)
    {
        if (license == null) throw new ArgumentNullException(nameof(license));

        IEnumerable<BranchDTO> branchesDTO;
        if (license.Branches is null)
        {
            branchesDTO = new List<BranchDTO>();
        }
        else
        {
            branchesDTO = license.Branches.Select(x => x.Branch.ToDTO());
        }

        return new LicenseDTO(
            license.Id,
            license.Designer.ToDTO(),
            license.Content,
            branchesDTO);
    }
}
