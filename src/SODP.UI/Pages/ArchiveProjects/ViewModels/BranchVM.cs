﻿namespace SODP.UI.Pages.ArchiveProjects.ViewModels;

public record BranchVM
{
    public int Id { get; set; }

    public string Sign { get; set; }

    public string Title { get; set; }

    public int Order { get; set; }

    public bool ActiveStatus { get; set; }
}