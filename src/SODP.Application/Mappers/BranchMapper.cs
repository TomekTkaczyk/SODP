using Newtonsoft.Json.Linq;
using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Application.Mappers;

public static class BranchMapper
{
    public static BranchDTO ToDTO(this Branch branch)
    {                                     
        if (branch == null) throw new ArgumentNullException(nameof(branch));

        IEnumerable<LicenseDTO> licensesDTO;
        if(branch.Licenses is null)
        {
           licensesDTO = new List<LicenseDTO>();
        } 
        else
        {
            licensesDTO = branch.Licenses.Select(x => x.License.ToDTO());
        }

        return new BranchDTO(
            branch.Id,
            branch.Sign,
            branch.Title,
            branch.Order,
            branch.ActiveStatus,
            licensesDTO);
    }
}
