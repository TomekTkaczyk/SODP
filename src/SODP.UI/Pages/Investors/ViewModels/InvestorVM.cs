using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Investors.ViewModels
{
	public class InvestorVM
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa inwestora jest wymagana")]
        public string Name { get; set; }
    }
}
