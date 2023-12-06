using SODP.Domain.ValueObjects;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using System;
using System.Collections.Generic;
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
    public IReadOnlyCollection<ProjectPart> Parts { get; private set; } = new List<ProjectPart>();

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

    public void AddPart(Part part)
    {
		_ = Parts.Append(ProjectPart.Create(this, part));
    }

	public override string ToString()
	{
		return Symbol + "_" + Name.Value.Trim();
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
		DateTime DevelopmentDate
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
