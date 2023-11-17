using SODP.Shared.DTO;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions;

public static class ProjectDTOExtension
{

    public static StringContent ToHttpContent(this NewProjectVM project)
    {
        return new StringContent(
                              JsonSerializer.Serialize(new NewProjectDTO
                              {
                                  Number = project.Number,
                                  StageSign = project.StageSign,
                                  Name = project.Name,
                              }),
                              Encoding.UTF8,
                              "application/json"
                          );
    }


    //public static StringContent ToHttpContent(this ProjectDTO project)
    //{
        //var projectDTO = new ProjectDTO
        //{
        //    Id = project.Id,
        //    Number = project.Number,
        //    Name = project.Name,
        //    Title = project.Title,
        //    Address = project.Address,
        //    LocationUnit = project.LocationUnit,
        //    BuildingCategory = project.BuildingCategory,
        //    Investor = project.Investor,
        //    BuildingPermit = project.BuildingPermit,
        //    Description = project.Description,
        //    DevelopmentDate = project.DevelopmentDate == null ? null : DateTime.Parse(project.DevelopmentDate),
        //    Status = project.Status, 
        //    Stage = new StageDTO
        //    {
        //        Id = project.StageId,
        //        Sign = project.StageSign,
        //        Name = project.StageName
        //    }
        //};

    //    return new StringContent(
    //                          JsonSerializer.Serialize(projectDTO),
    //                          Encoding.UTF8,
    //                          "application/json"
    //                      );
    //}


}
