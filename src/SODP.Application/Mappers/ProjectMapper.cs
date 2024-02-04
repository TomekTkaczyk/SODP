using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Application.Mappers;

public static class ProjectMapper
{
    public static ProjectDTO ToDTO(this Project project)
    {
        if(project == null) throw new ArgumentNullException(nameof(project));

        return new ProjectDTO {
            Id = project.Id,
            Number = (project.Number is not null) ? project.Number : "",
            Stage = project.Stage.ToDTO(),
            Name = (project.Name is not null) ? project.Name : "",
            Title = (project.Title is not null) ? project.Title : "",
            Address = (project.Address is not null) ? project.Address : "",
            LocationUnit = project.LocationUnit,
            BuildingCategory = project.BuildingCategory,
            BuildingPermit = project.BuildingPermit,
            Investor = project.Investor,
            Description = project.Description,
            DevelopmentDate = project.DevelopmentDate,
            Status = project.Status,
            Parts = (project.Parts is not null) 
                ? project.Parts.Select(x => x.ToDTO()) 
                : new List<ProjectPartDTO>()    
        };
    }
}
