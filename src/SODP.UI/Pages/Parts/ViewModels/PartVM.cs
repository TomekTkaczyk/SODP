using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Parts.ViewModels
{
	public class PartVM
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "Znak części jest wymagany")]
        [MinLength(1)]
        public string Sign { get; set; }

        [Required(ErrorMessage = "Nazwa części jest wymagana")]
        public string Name { get; set; }
    }
}
