using SODP.Shared.DTO;
using SODP.UI.Pages.Parts.ViewModels;
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
									  Title = part.Title
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
                Title = part.Title,
            };
        }
    }
}
