﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class NewLicenseVM
    {
        public int DesignerId { get; set; }
    
        public string Content { get; set; }

        public SelectListItem Branch { get; set; }

        public int BranchId { get; set; } 

        public List<SelectListItem> Branches { get; set; }

        public List<SelectListItem> ApplyBranches { get; set; }

    }
}