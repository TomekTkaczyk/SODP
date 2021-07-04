using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO
{
    public class StageDTO  : BaseDTO
    {
        [Required]
        [MinLength(2)]
        public string Sign { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
