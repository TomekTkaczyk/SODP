using SODP.Domain.Entities;
using SODP.Shared.DTO;

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
                    Name = stage.Name
                };
            }

            return null;
        }
    }
}
