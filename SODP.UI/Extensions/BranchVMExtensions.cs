using SODP.Shared.DTO;
using SODP.UI.Pages.Branches.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
    public static class BranchVMExtensions
    {
        public static StringContent ToHttpContent(this BranchVM branch)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new BranchDTO
                                  {
                                      Id = branch.Id,
                                      Symbol = branch.Symbol,
                                      Sign = branch.Sign,
                                      Name = branch.Name
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static BranchVM ToViewModel(this BranchDTO branch)
        {
            return new BranchVM
            {
                Id = branch.Id,
                Symbol = branch.Symbol,
                Sign = branch.Sign,
                Name = branch.Name
            };
        }
    }
}
