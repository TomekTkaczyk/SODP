using SODP.Shared.DTO;
using SODP.UI.Pages.Designers.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
    public static class DesignerVMExtensions
    {
        public static StringContent ToHttpContent(this DesignerVM designer)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new DesignerDTO
                                  {
                                      Id = designer.Id,
                                      Title = designer.Title,
                                      Firstname = designer.Firstname,
                                      Lastname = designer.Lastname

                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static DesignerVM ToViewModel(this DesignerDTO designer)
        {
            return new DesignerVM
            {
                Id = designer.Id,
                Title = designer.Title,
                Firstname = designer.Firstname,
                Lastname = designer.Lastname
            };
        }
    }
}
