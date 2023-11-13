using SODP.Shared.Enums;
using SODP.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SODP.Domain.Entities;

public class Project : BaseEntity
{
    private Project() { }

    private Project(string number, Stage stage, string name)
    {
        RequiredPropertiesInit(number, stage, name);
        EmptyPropertiesInit();
    }

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

    public static Project Create(string number, Stage stage, string name)
    {
        return new Project(number, stage, name);
    }

    public string Number { get; set; }           // Project number
    public int StageId { get; set; }             // Project stage Id
    public Stage Stage { get; set; }             // Stage object
    public string Name { get; set; }             // Project name (file system - directory name)
    public string Title { get; set; }            // The name of the construction project 
    public string Address { get; set; }          // Address
    public string LocationUnit { get; set; }     // Location class unit
    public string BuildingCategory { get; set; }
    public string Investor { get; set; }
    public string BuildingPermit { get; set; }
    public string Description { get; set; }     
    public DateTime? DevelopmentDate { get; set; }
    public ProjectStatus Status { get; set; }
    public IReadOnlyCollection<ProjectPart> Parts { get; private set; } = new List<ProjectPart>();

    public virtual string Symbol 
    {
        get 
        {
            return Number.Trim() + Stage.Sign.Trim();
        }
    }

    public override string ToString()
    {
        return Symbol + "_" + Name.Trim();
    }

    public void Normalize()
    {
        var regex = new Regex("[ ]", RegexOptions.None);
        Name = regex.Replace(Name, "_");
        regex = new Regex("[_]{2,}", RegexOptions.None);
        Name = regex.Replace(Name, "_");
        regex = new Regex("(_+)$", RegexOptions.None);
        Name = regex.Replace(Name, "");
        Name = Name.CapitalizeFirstLetter();
    }

    private void EmptyPropertiesInit()
    {
        Address = "";
        Investor = "";
    }

    private void RequiredPropertiesInit(string number, Stage stage, string name)
    {
        Number = number;
        Stage = stage;
        Name = name;
    }

    public void AddPart(Part part)
    {
        Parts.Append(new ProjectPart(this, part));
    }
}
