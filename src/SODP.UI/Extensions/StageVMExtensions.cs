using SODP.Shared.DTO;
using SODP.UI.Pages.Stages.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
    public static class StageVMExtensions
    {
        public static StringContent ToHttpContent(this StageVM stage)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new StageDTO
                                  {
                                      Id = stage.Id,
                                      Sign = stage.Sign,
                                      Name = stage.Name
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static StageVM ToViewModel(this StageDTO stage)
        {
            return new StageVM
            {
                Id = stage.Id,
                Sign = stage.Sign,
                Name = stage.Name
            };
        }
    }
}
