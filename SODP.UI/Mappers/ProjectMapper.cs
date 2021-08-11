using SODP.Shared.DTO;
using SODP.UI.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Mappers
{
    public static class ProjectMapper
    {
        public static StringContent ToHttpContent(this NewProjectVM project)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new NewProjectDTO
                                  {
                                      Number = project.Number,
                                      StageId = project.StageId,
                                      Title = project.Title,
                                      Description = project.Description
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static StringContent ToHttpContent(this ProjectVM project)
        {
            var jsonBody = JsonSerializer.Serialize(new ProjectDTO
            {
                Id = project.Id,
                Number = project.Number,
                Stage = new StageDTO
                {
                    Id = project.StageId,
                    Sign = project.StageSign,
                    Title = project.StageTitle
                },
                Title = project.Title,
                Description = project.Description,
                Investment = project.Investment,
                TitleStudy = project.TitleStudy,
                Address = project.Address,
                Investor = project.Investor                
            });

            return new StringContent(jsonBody, Encoding.UTF8, "application/json" );
        }
    }
}
