using SODP.Shared.DTO;
using SODP.UI.Pages.ActiveProjects;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Extensions
{
    public static class ProjectVMExtension
    {

        public static StringContent ToHttpContent(this NewProjectVM project)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new NewProjectDTO
                                  {
                                      Number = project.Number,
                                      StageId = (int)project.StageId,
                                      Name = project.Name,
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }


        public static StringContent ToHttpContent(this ProjectVM project)
        {
            var projectDTO = new ProjectDTO
            {
                Id = project.Id,
                Number = project.Number,
                Name = project.Name,
                Title = project.Title,
                Address = project.Address,
                LocationUnit = project.LocationUnit,
                BuildingCategory = project.BuildingCategory,
                Investor = project.Investor,
                Description = project.Description,
                Status = project.Status, 
                Stage = new StageDTO
                {
                    Id = project.StageId,
                    Sign = project.StageSign,
                    Name = project.StageName
                }
            };

            return new StringContent(
                                  JsonSerializer.Serialize(projectDTO),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }


    }
}
