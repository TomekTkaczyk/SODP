using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions;

public static class VMExtensions
{
    public static StringContent ToHttpContent<T>(this T model)
    {
        return new StringContent(
             JsonSerializer.Serialize(model),
             Encoding.UTF8,
             "application/json"
         );
    } 
}
