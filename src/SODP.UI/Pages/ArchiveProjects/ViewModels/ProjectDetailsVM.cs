using SODP.UI.Pages.Shared.PageModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.ArchiveProjects.ViewModels;

public class ProjectDetailsVM
{
    public int Id { get; set; }

    public string Number { get; set; }

    public StageVM Stage { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string Address { get; set; }

    public string LocationUnit { get; set; }

    public string BuildingCategory { get; set; }

    public string Investor { get; set; }

    public string BuildingPermit { get; set; }

    public string Description { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateTime? DevelopmentDate { get; set; }

    public ICollection<ProjectPartVM> Parts { get; set; }
}
