
using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO
{
    public class BranchDTO  : BaseDTO
    {
        [Required]
        [MinLength(1)]
        public string Sign { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
