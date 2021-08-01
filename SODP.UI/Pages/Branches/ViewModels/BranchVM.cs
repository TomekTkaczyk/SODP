using SODP.Shared.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Pages.Branches.ViewModels
{
    public class BranchVM : NewBranchVM
    {
        public int Id { get; set; }

        public override StringContent ToHttpContent()
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new BranchDTO
                                  {
                                      Id = this.Id,
                                      Sign = this.Sign,
                                      Title = this.Title
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
