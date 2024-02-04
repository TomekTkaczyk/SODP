using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.ComponentModel;
using System.Linq;

namespace SODP.Application.Mappers;

public static class DesignerMapper
{
    public static DesignerDTO ToDTO(this Designer designer)
    {
        if (designer == null) throw new ArgumentNullException(nameof(designer));

        return new DesignerDTO(
            designer.Id,
            designer.Title,
            designer.Firstname,
            designer.Lastname,
            designer.ActiveStatus,
            designer.Licenses.Select(x => x.ToDTO()).ToList());
    }
}
