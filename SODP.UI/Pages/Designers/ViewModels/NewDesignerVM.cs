using SODP.Shared.DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;

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
    }
}
