using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.ViewModels
{
    public class BranchVM
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Sign { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
