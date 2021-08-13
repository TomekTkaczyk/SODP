using SODP.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.ViewModels
{
    public class ProjectVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numer projektu jest wymagany.")]
        [RegularExpression(@"^([1-9]{1})([0-9]{3})$", ErrorMessage = "Numer projektu powinien zawierać 4 cyfry.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Stadium jest wymagane.")]
        public int StageId { get; set; }

        public string StageSign { get; set; }

        public string StageTitle { get; set; }

        [Required(ErrorMessage = "Tytuł projektu jest wymagany.")]
        [RegularExpression(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$", ErrorMessage = "Tytuł musi zaczynać się literą, może zawierać podkreślenie, spacje, cyfry oraz litery bez polskich znaków diakrytycznych")]
        public string Title { get; set; }

        public string Description { get; set; }

        public ProjectStatus Status { get; set; }

        public string Stage { get { return $"({StageSign}) {StageTitle}"; } }
        
        public string  Investment { get; set; }

        public string TitleStudy { get; set; }

        public string Address { get; set; }

        public string Investor { get; set; }

        public List<BranchVM> Branches { get; set; }

    }
}
