﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SODP.Domain.DTO
{
    public class ProjectDTO : BaseDTO
    {
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

        public string Stage { get { return $"({StageSign}) {StageTitle}"; } }
    }
}
