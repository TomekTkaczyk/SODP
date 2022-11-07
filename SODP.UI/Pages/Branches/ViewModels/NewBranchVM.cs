using SODP.Shared.DTO;
using SODP.UI.Mappers;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Pages.Branches.ViewModels
{
    public class NewBranchVM
    {

        [Required(ErrorMessage ="Oznaczenie jest wymagane")]
        [RegularExpression(@"^([0-9]{2})$", ErrorMessage = "Oznaczenie powinno zawierać 2 cyfry.")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "Znak branży jest wymagany")]
        [MinLength(1)]
        public string Sign { get; set; }

        [Required(ErrorMessage = "Nazwa branży jest wymagana")]
        public string Name { get; set; }
    }
}
