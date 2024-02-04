using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;

namespace SODP.Application.Mappers;

public static class StageMapper
{
    public static StageDTO ToDTO(this Stage stage)
    {
        if (stage == null) throw new ArgumentNullException(nameof(stage));
        
        return new StageDTO(
                stage.Id,
                stage.Sign,
                stage.Title,
                stage.ActiveStatus);
    }
}
