using SODP.Shared.DTO;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
    public static class NewPartVMExtensions
    {
        public static StringContent ToHttpContent(this NewPartVM part)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new PartDTO
                                  {
                                      Sign = part.Sign,
                                      Name = part.Name
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static NewPartVM ToViewModel(this PartDTO part)
        {
            return new NewPartVM
            {
                Sign = part.Sign,
                Name = part.Name
            };
        }
    }
}
