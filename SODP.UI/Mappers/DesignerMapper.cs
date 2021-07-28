using SODP.Shared.DTO;
using SODP.UI.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Mappers
{
    public static class DesignerMapper
    {
        public static StringContent ToHttpContent(this NewDesignerVM designer)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new NewDesignerDTO
                                  {
                                      Title = designer.Title,
                                      Firstname = designer.Firstname,
                                      Lastname = designer.Lastname
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
