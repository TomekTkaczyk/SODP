using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Exceptions.StageExceptions;
using SODP.Domain.Exceptions.ValueObjectExceptions;
using SODP.Domain.ValueObjects;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SODP.Domain.Entities;

public class Project : BaseEntity
{
    private Project() { }

    private Project(ProjectNumber number, Stage stage, ProjectName name)
    {
        RequiredPropertiesInit(number, stage, name);
        EmptyPropertiesInit();
    }

    public ProjectNumber Number { get; private set; }	// Project number
    public int StageId { get; private set; }            // Project stage Id
    public Stage Stage { get; private set; }            // Stage object
    public string Name { get; private set; }			// Project name (file system - directory name)
    public string Title { get; private set; }                   // The name of the construction project 
    public string Address { get; private set; }                 // Address
    public string LocationUnit { get; private set; }            // Location class unit
    public string BuildingCategory { get; private set; }
    public string Investor { get; private set; }
    public string BuildingPermit { get; private set; }
    public string Description { get; private set; }     
    public DateTime? DevelopmentDate { get; private set; }
    public ProjectStatus Status { get; private set; }
    public IReadOnlyCollection<ProjectPart> Parts { get; private set; } = new List<ProjectPart>();
    
	public virtual string Symbol => Number.Value.Trim() + Stage.Sign.Value.Trim();

	public static Project Create(string number, Stage stage, string name)
	{															    

		return new Project(number, stage, name.ToUpper());
	}

	public void ChangeStatus(ProjectStatus status)
    {
        Status = status;
    }

    public void AddPart(Part part)
    {
		_ = Parts.Append(ProjectPart.Create(this, part));
    }

	public override string ToString()
	{
		return Symbol + "_" + Name.Trim();
	}

	public void Update(ProjectDTO project)
	{
		this.Name = string.IsNullOrEmpty(project.Name) ? "" : project.Name.ToUpper();
		this.Title = string.IsNullOrEmpty(project.Title) ? "" :	project.Title.ToUpper();
		this.Description = string.IsNullOrEmpty(project.Description) ? "" : project.Description.ToUpper();
		this.Address = string.IsNullOrEmpty(project.Address) ? "" : project.Address.ToUpper();
		this.BuildingCategory = string.IsNullOrEmpty(project.BuildingCategory) ? "" : project.BuildingCategory.ToUpper();
		this.BuildingPermit = string.IsNullOrEmpty(project.BuildingPermit) ? "" : project.BuildingPermit.ToUpper();
		this.LocationUnit = string.IsNullOrEmpty(project.LocationUnit) ? "" : project.LocationUnit.ToUpper();
		this.Investor = string.IsNullOrEmpty(project.Investor) ? "" : project.Investor.ToUpper();
		this.DevelopmentDate = project.DevelopmentDate;
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


	//private void Normalize()
	//{
	//	var regex = new Regex("[ ]", RegexOptions.None);
	//	Name = regex.Replace(Name, "_");
	//	regex = new Regex("[_]{2,}", RegexOptions.None);
	//	Name = regex.Replace(Name, "_");
	//	regex = new Regex("(_+)$", RegexOptions.None);
	//	Name = regex.Replace(Name, "");
	//	Name = Name.CapitalizeFirstLetter();
	//}


	//public Project(string foldername)
	//{
	//    var sign = foldername.GetUntilOrEmpty("_");
	//    RequiredPropertiesInit(
	//        sign[..4],
	//        sign[4..],
	//        foldername[(sign.Length + 1)..]);
	//    EmptyPropertiesInit();

	//    if (string.IsNullOrEmpty(Stage.Sign))
	//    {
	//        sign = foldername.GetLastUntilOrEmpty("_");
	//        Stage = Stage.Create(sign, "");
	//        var nameLength = Name.Length - Stage.Sign.Length - 1;
	//        Name = Name[0..nameLength];
	//    }
	//}
}
