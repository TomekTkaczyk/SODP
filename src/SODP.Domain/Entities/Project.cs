using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.ValueObjects;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SODP.Domain.Entities;

public class Project : BaseEntity
{
    public ProjectNumber Number { get; private set; }	// Project number
    public int StageId { get; private set; }            // Project stage Id
    public Stage Stage { get; private set; }            // Stage object
    public ProjectName Name { get; private set; }		// Project name (file system - directory name)
    public Title Title { get; private set; }            // Short description 
    public Address Address { get; private set; }        // Address
    public string LocationUnit { get; private set; }    // Location class unit
    public string BuildingCategory { get; private set; }
    public string Investor { get; private set; }
    public string BuildingPermit { get; private set; }
    public string Description { get; private set; }     
    public DateTime? DevelopmentDate { get; private set; }
    public ProjectStatus Status { get; private set; }
    public ICollection<ProjectPart> Parts { get; private set; }

	private Project() { }

	private Project(ProjectNumber number, Stage stage, ProjectName name)
    {
        RequiredPropertiesInit(number, stage, name);
        EmptyPropertiesInit();
    }

    
	public virtual string Symbol => Number.Value.Trim() + Stage.Sign.Value.Trim();

	public static Project Create(string number, Stage stage, string name)
	{															    

		return new Project(number, stage, name);
	}

	public void ChangeStatus(ProjectStatus status)
    {
        Status = status;
    }

    public void AddPart(Sign sign, Title title)
    {
		if(Parts.Any(x => x.Sign.Equals(sign)))
		{
			throw new ProjectPartConflictException();
		}

        Parts.Add(ProjectPart.Create(this, sign, title));
    }

	public void UpdatePart(int PartId, Sign sign, Title title)
	{
		if (Parts.Any(x => x.Id != PartId && x.Sign.Equals(sign)))
		{
			throw new ProjectPartConflictException();
		}

		var part = Parts.FirstOrDefault(x => x.Id == PartId) 
			?? throw new ProjectPartNotFoundException();
		
		part.Update(sign, title);
	}

	private void EmptyPropertiesInit()
	{
		Address = "";
		Investor = "";
		Description = "";
	}

	private void RequiredPropertiesInit(ProjectNumber number, Stage stage, ProjectName name)
	{
		Number = number;
		Stage = stage;
		Name = name;
	}

	public void Update(
		ProjectName Name,
		Title Title,
		Address Address,
		string LocationUnit,
		string BuildingCategory,
		string Investor,
		string BuildingPermit,
		string Description,
		DateTime? DevelopmentDate
	)
	{
		this.Name = string.IsNullOrEmpty(Name) ? "" : Name;
		this.Title = string.IsNullOrEmpty(Title) ? "" : Title;
		this.Address = string.IsNullOrEmpty(Address) ? "" : Address;
		this.LocationUnit = string.IsNullOrEmpty(LocationUnit) ? "" : LocationUnit;
		this.BuildingCategory = string.IsNullOrEmpty(BuildingCategory) ? "" : BuildingCategory;
		this.Investor = string.IsNullOrEmpty(Investor) ? "" : Investor;
		this.BuildingPermit = string.IsNullOrEmpty(BuildingPermit) ? "" : BuildingPermit;
		this.Description = string.IsNullOrEmpty(Description) ? "" : Description;
		this.DevelopmentDate = DevelopmentDate;
	}
	
	public override string ToString()
	{
		return Symbol + "_" + Name.Value.Trim();
	}
}
