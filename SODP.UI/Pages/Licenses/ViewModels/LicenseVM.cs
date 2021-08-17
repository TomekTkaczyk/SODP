using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace SODP.UI.Pages.Licenses.ViewModels
{
    public class LicenseVM
    {
        public int Id { get; set; }

        public int DesignerId { get; set; }

        public string Designer { get; set; }

        [Required]
        public string Content { get; set; }

        public SelectListItem Branch { get; set; }

        public int BranchId { get; set; }

        public List<SelectListItem> Branches { get; set; }

        public List<SelectListItem> ApplyBranches { get; set; }

    }
}
