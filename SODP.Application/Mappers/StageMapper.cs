using SODP.Model;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Application.Mappers
{
    public static class StageMapper
    {
        public static StageDTO ToDTO(this Stage stage)
        {
            if(stage != null)
            {
                return new StageDTO
                {
                    Id = stage.Id,
                    Sign = stage.Sign,
                    Title = stage.Title
                };
            }

            return null;
        }
    }
}
