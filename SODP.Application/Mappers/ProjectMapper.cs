using Microsoft.EntityFrameworkCore.Query;
using SODP.Model;
using SODP.Shared.DTO;
using System.Linq;

namespace SODP.Application.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDTO ToDTO(this Project project)
        {
            if(project != null)
            {
                var result = new ProjectDTO
                {
                    Id = project.Id,
                    Number = project.Number,
                    Stage = project.Stage.ToDTO(),
                    Name = project.Name,
                    Title = project.Title,
                    Address = project.Address,
                    LocationUnit = project.LocationUnit,
                    BuildingCategory = project.BuildingCategory,
                    Investor = project.Investor,
                    Description = project.Description,
                    Parts = (from part in project.Parts select new ProjectPartDTO()
                    {
                       Id = part.Id,
                    }).ToList()
                };

                return result;
            }

            return null;
        }
    }
}
