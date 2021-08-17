using SODP.Shared.DTO;
using SODP.UI.Pages.ActiveProjects;
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

        public static StringContent ToHttpContent(this ProjectVM project)
        {
            var projectDTO = new ProjectDTO
            {
                Id = project.Id,
                Title = project.Title,
                Address = project.Address,
                Description = project.Description,
                Investment = project.Investment,
                Investor = project.Investor,
                Number = project.Number,
                TitleStudy = project.TitleStudy,
                Status = project.Status, 
                Stage = new StageDTO
                {
                    Id = project.StageId,
                    Sign = project.StageSign,
                    Title = project.Title
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
