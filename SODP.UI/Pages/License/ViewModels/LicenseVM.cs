using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SODP.UI.Pages.License.ViewModels
{
    public class LicenseVM
    {
        public int Id { get; set; }

        public int DesignerId { get; set; }

        public string Designer { get; set; }

        public string Content { get; set; }

        public SelectListItem Branch { get; set; }

        public int BranchId { get; set; }

        public List<SelectListItem> Branches { get; set; }

        public List<SelectListItem> ApplyBranches { get; set; }

        internal StringContent ToHttpContent()
        {
            throw new NotImplementedException();
        }
    }
}
