using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class NewLicenseVM
    {
        public int DesignerId { get; set; }

        [Required(ErrorMessage = "Wpisz nr uprawnień")] 
        public string Content { get; set; }

    }
}
