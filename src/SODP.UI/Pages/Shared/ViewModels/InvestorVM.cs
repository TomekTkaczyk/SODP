using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Shared.ViewModels
{
    public class InvestorVM
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa inwestora jest wymagana")]
        public string Name { get; set; }
        public bool ActiveStatus { get; set; }

    }
}
