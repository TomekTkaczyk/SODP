using SODP.Model.Extensions;
using SODP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SODP.Model;

public class Project : BaseEntity
{
    public Project() { }

    public Project(string number, string stageSign, string name)
    {
        RequiredPropertiesInit(number, stageSign, name);
        EmptyPropertiesInit();
    }

    public Project(string foldername)
    {
        var sign = foldername.GetUntilOrEmpty("_");
        RequiredPropertiesInit(
            sign[..4],
            sign[4..],
            foldername[(sign.Length + 1)..]);
        EmptyPropertiesInit();

        if (string.IsNullOrEmpty(Stage.Sign))
        {
            sign = foldername.GetLastUntilOrEmpty("_");
            Stage.Sign = sign;
            var nameLength = Name.Length - Stage.Sign.Length - 1;
            Name = Name[0..nameLength];
        }
    }


    public string Number { get; set; }           // Project number
    public int StageId { get; set; }             // Project stage Id
    public virtual Stage Stage { get; set; }     // Stage object
    public string Name { get; set; }             // Project name (file system - directory name)
    public string Title { get; set; }            // The name of the construction project 
    public string Address { get; set; }         // Address
    public string LocationUnit { get; set; }     // Location record unit
    public string BuildingCategory { get; set; }
    public string Investor { get; set; }
    public string BuildingPermit { get; set; }
    public string Description { get; set; }     
    public DateTime? DevelopmentDate { get; set; }
    public ProjectStatus Status { get; set; }
    public ICollection<ProjectPart> Parts { get; set; }

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
        Regex regex = new Regex("[ ]", RegexOptions.None);
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

    private void RequiredPropertiesInit(string number, string stageSign, string name)
    {
        Number = number;
        Stage = new Stage(stageSign);
        Name = name;
    }
}
