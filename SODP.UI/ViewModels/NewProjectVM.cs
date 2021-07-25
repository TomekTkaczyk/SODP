using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.ViewModels
{
    public class NewProjectVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numer projektu jest wymagany.")]
        [RegularExpression(@"^([1-9]{1})([0-9]{3})$", ErrorMessage = "Numer projektu powinien zawierać 4 cyfry.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Tytuł projektu jest wymagany.")]
        [RegularExpression(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$", ErrorMessage = "Tytuł musi zaczynać się literą, może zawierać podkreślenie, spacje, cyfry oraz litery bez polskich znaków diakrytycznych")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Stadium jest wymagane.")]
        public int StageId { get; set; }

        public IEnumerable<SelectListItem> Stages { get; set; }
    }
}
