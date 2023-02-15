using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Branches.ViewModels
{
    public class NewBranchVM
    {

        [Required(ErrorMessage = "Znak branży jest wymagany")]
        [MinLength(1)]
        public string Sign { get; set; }

        [Required(ErrorMessage = "Nazwa branży jest wymagana")]
        public string Name { get; set; }
    }
}
