using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO
{
    public class NewProjectDTO : BaseDTO
    {
        [Required(ErrorMessage = "Numer projektu jest wymagany")]
        [RegularExpression(@"^([1-9]{1})([0-9]{3})$", ErrorMessage = "Numer projektu powinien zawierać 4 cyfry")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Stadium jest wymagane.")]
        public int StageId { get; set; }

        [Required(ErrorMessage = "Nazwa projektu jest wymagana")]
        [RegularExpression(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$", ErrorMessage = "Tytuł musi zaczynać się literą, może zawierać podkreślenie, spacje, cyfry oraz litery bez polskich znaków diakrytycznych")]
        public string Name { get; set; }

    }
}
