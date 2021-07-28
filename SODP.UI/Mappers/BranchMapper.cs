using SODP.Shared.DTO;
using SODP.UI.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Mappers
{
    public static class BranchMapper
    {
        public static StringContent ToHttpContent(this NewBranchVM branch)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new NewBranchDTO
                                  {
                                      Sign = branch.Sign,
                                      Title = branch.Title
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static StringContent ToHttpContent(this BranchVM branch)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new BranchDTO
                                  {
                                      Id = branch.Id,
                                      Sign = branch.Sign,
                                      Title = branch.Title
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
