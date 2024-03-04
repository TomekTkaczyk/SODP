using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Application.Mappers;

public static class LicenseMapper
{
    public static LicenseDTO ToDTO(this License license, List<object> mapCache = null)
    {
        if(license == null) {
            return null;
        }

        mapCache ??= new List<object>();

        if(mapCache.Contains(license)) {
            return new LicenseDTO(
                license.Id,
                default,
                license.Content,
				default);
        }

        mapCache.Add(license);

		return new LicenseDTO(
            license.Id,
            license.Designer.ToDTO(mapCache),
            license.Content,
			(license.Branches is not null) 
                ? license.Branches.Select(x => x.Branch.ToDTO(mapCache)) 
                : default);
    }
}
