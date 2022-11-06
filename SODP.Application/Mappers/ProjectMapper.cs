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
                return new ProjectDTO
                {
                    Id = project.Id,
                    Number = project.Number,
                    Stage = project.Stage.ToDTO(),
                    Name = project.Name,
                    Title = project.Title,
                    Address = project.Address,
                    LocationUnit = project.LocationUnit,
                    Investor = project.Investor,
                    Description = project.Description,
                    Branches = (from branch in project.Branches select new ProjectBranchDTO()
                    {
                       Id = branch.Id,
                       BranchId = branch.BranchId,
                       Name = branch.ToString()
                    }).ToList()
                };
            }

            return null;
        }
    }
}
