﻿using System.Collections.Generic;

namespace SODP.UI.Pages.ArchiveProjects.ViewModels;

public class ProjectPartVM
{
    public int Id { get; set; }

    public string Sign { get; set; }

    public string Title { get; set; }

    public int Order { get; set; }

    public ICollection<PartBranchVM> Branches { get; set; }
}