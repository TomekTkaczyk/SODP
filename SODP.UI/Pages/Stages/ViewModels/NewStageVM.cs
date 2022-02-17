using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Stages.ViewModels
{
    public class NewStageVM
    {
        [Required(ErrorMessage = "Symbol jest wymagany")]
        public string Sign { get; set; }
        
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }

    }
}
