using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SODP.Application.Mappers;

public static class DesignerMapper
{
    public static DesignerDTO ToDTO(this Designer designer, List<object> mapCache = null)
    {
        if(designer == null) return null;

        mapCache ??= new List<object>();

        if(mapCache.Contains(designer)) {
            return new DesignerDTO(
                designer.Id,
			    designer.Title,
			    designer.Firstname,
			    designer.Lastname,
			    designer.ActiveStatus,
                default);
        }

        var designerDTO = new DesignerDTO(
            designer.Id,
            designer.Title,
            designer.Firstname,
            designer.Lastname,
            designer.ActiveStatus,
            designer.Licenses.Select(x => x.ToDTO(mapCache)));

        mapCache.Add(designer);

        return designerDTO;
    }
}
