using SODP.Shared.DTO;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
    public static class PartVMExtensions
    {
        public static StringContent ToHttpContent(this PartVM part)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new PartDTO
                                  {
                                      Id= part.Id,
                                      Sign = part.Sign,
                                      Name = part.Name,
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static PartVM ToViewModel(this PartDTO part)
        {
            return new PartVM
            {
                Id = part.Id,
                Sign = part.Sign,
                Name = part.Name,
            };
        }
    }
}
