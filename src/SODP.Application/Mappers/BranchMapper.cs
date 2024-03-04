using Newtonsoft.Json.Linq;
using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SODP.Application.Mappers;

public static class BranchMapper
{
    public static BranchDTO ToDTO(this Branch branch, List<object> mapCache = null)
    {
        if(branch == null) {
            return null;
        }

        mapCache ??= new List<object>();

        if(mapCache.Contains(branch)) {
            return new BranchDTO(
                branch.Id,
                branch.Sign,
                branch.Title,
                branch.Order,
                branch.ActiveStatus,
                default);
		}

        mapCache.Add(branch);

		return new BranchDTO(
            branch.Id,
            branch.Sign,
            branch.Title,
            branch.Order,
		    branch.ActiveStatus,
            (branch.Licenses is not null) 
                ? branch.Licenses.Select(x => x.License.ToDTO()) 
                : default);
    }
}
