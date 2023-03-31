using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Investors.ViewModels;

public record InvestorVM
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Nazwa inwestora jest wymagana")]
	public string Name { get; set; }

	public bool ActiveStatus { get; set; }
}
