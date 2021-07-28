using SODP.Shared.DTO;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.ViewModels
{
    public class NewDesignerVM
    {
        //public NewDesignerVM()
        //{
        //    Title = "";
        //    Firstname = "";
        //    Lastname = "";
        //}

        [Required]
        public string Title { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
