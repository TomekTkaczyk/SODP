using SODP.Shared.DTO;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Pages.Designers.ViewModels
{
    public class NewDesignerVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }

        public virtual StringContent ToHttpContent()
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new NewDesignerDTO
                                  {
                                      Title = this.Title,
                                      Firstname = this.Firstname,
                                      Lastname = this.Lastname
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
