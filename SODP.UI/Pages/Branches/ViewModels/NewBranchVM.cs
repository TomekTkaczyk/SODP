using SODP.Shared.DTO;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Pages.Branches.ViewModels
{
    public class NewBranchVM
    {
        [Required]
        [MinLength(1)]
        public string Sign { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual StringContent ToHttpContent()
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new NewBranchDTO
                                  {
                                      Sign = this.Sign,
                                      Title = this.Title
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
