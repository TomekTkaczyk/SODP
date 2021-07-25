using SODP.Model;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    Description = project.Description,
                    Title = project.Title,
                    Branches = (from branch in project.Branches select new ProjectBranchDTO()
                    {
                       Id = branch.Id,
                       Name = branch.ToString()
                    }).ToList()
                };

            }

            return null;
        }
    }
}
