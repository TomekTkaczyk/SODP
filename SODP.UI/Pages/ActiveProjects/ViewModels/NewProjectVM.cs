using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.ActiveProjects.ViewModels
{
    public class NewProjectVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numer projektu jest wymagany")]
        [RegularExpression(@"^([1-9]{1})([0-9]{3})$", ErrorMessage = "Numer projektu powinien zawierać 4 cyfry")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Nazwa projektu jest wymagana")]
        [RegularExpression(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$", ErrorMessage = "Nazwa musi zaczynać się literą, może zawierać podkreślenie, spacje, cyfry oraz litery bez polskich znaków diakrytycznych")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Stadium jest wymagane")]
        public int StageId { get; set; }

        public IEnumerable<SelectListItem> Stages { get; set; }
    }
}
