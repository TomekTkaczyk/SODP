﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.ActiveProjects.ViewModels;

public class PartVM
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    [Required]
    public string Sign { get; set; }

    [Required]
    public string Title { get; set; }

    public IList<SelectListItem> Items { get; set; }
}