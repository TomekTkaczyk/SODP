﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SODP.UI.Pages.ActiveProjects.ViewModels;

public class ProjectPartEditVM
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Sign { get; set; }

    public string Title { get; set; }

    public IList<SelectListItem> Items { get; set; }
}