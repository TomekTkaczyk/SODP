using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Shared.ViewModels
{
    public class PartVM
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Required]
        public string Sign { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<SelectListItem> Items { get; set; }
    }
}
