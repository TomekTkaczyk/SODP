using SODP.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class ProjectVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numer projektu jest wymagany")]
        [RegularExpression(@"^([1-9]{1})([0-9]{3})$", ErrorMessage = "Numer projektu powinien zawierać 4 cyfry.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Stadium jest wymagane")]
        public int StageId { get; set; }
        public string StageSign { get; set; }
        public string StageName { get; set; }
        public string Stage { get { return $"({StageSign}) {StageName}"; } }

        [Required(ErrorMessage = "Nazwa projektu jest wymagana")]
        [RegularExpression(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$", ErrorMessage = "Tytuł musi zaczynać się literą, może zawierać podkreślenie, spacje, cyfry oraz litery bez polskich znaków diakrytycznych")]
        public string Name { get; set; }
                                                 
        public string Title { get; set; }

        public string Address { get; set; }
        
        public string LocationUnit { get; set; }

        public string BuildingCategory { get; set; }

        public string Investor { get; set; }
        
        public string Description { get; set; }
        
        public ProjectStatus Status { get; set; }

    }
}
