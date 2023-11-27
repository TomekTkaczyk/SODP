using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.Stages.ViewModels
{
    public class StageVM
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Symbol jest wymagany")]
		public string Sign { get; set; }

		public string Title { get; set; }

		public bool ActiveStatus { get; set; }
	}
}
