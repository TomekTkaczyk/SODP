using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class NewDesignerVM
    {
        [DefaultValue("")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Lastname { get; set; }

        public bool ActiveStatus { get; set; }
    }
}
