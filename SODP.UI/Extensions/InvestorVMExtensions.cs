using SODP.Shared.DTO;
using SODP.UI.Pages.Investors.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
    public static class InvestorVMExtensions
    {
        public static StringContent ToHttpContent(this InvestorVM investor)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new InvestorDTO
                                  {
                                      Id = investor.Id,
                                      Name = investor.Name
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static InvestorVM ToViewModel(this InvestorDTO investor)
        {
            return new InvestorVM
            {
                Id = investor.Id,
                Name = investor.Name
            };
        }
    }
}
