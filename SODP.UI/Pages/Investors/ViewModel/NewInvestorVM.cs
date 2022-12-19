using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Investors.ViewModel
{
	public class NewInvestorVM
	{
		[Required(ErrorMessage = "Nazwa inwestora jest wymagana")]
		public string Name { get; set; }
	}
}
