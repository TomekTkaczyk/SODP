using SODP.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO
{
    public class ProjectDTO : BaseDTO
    {
        [Required(ErrorMessage = "Numer projektu jest wymagany.")]
        [RegularExpression(@"^([1-9]{1})([0-9]{3})$", ErrorMessage = "Numer projektu powinien zawierać 4 cyfry.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Tytuł projektu jest wymagany.")]
        [RegularExpression(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$", ErrorMessage = "Tytuł musi zaczynać się literą, może zawierać podkreślenie, spacje, cyfry oraz litery bez polskich znaków diakrytycznych")]
        public string Title { get; set; }

        public string Description { get; set; }

        public StageDTO Stage { get; set; }

        public ProjectStatus Status { get; set; }

        public ICollection<ProjectBranchDTO> Branches { get; set; }

        public override string ToString()
        {
            return $"{Number.Trim()}{Stage.Sign} {Title.Trim()}";
        }
    }
}
