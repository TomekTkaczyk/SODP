using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class DesignerEditVM
    {
        public int Id { get; set; }

        [DefaultValue("")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Lastname { get; set; }

    }
}
