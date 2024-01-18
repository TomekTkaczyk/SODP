using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace SODP.UI.Extensions;

public static class VMExtensions
{
    public static StringContent ToHttpContent<T>(this T model)
    {
		var jsonSettings = new JsonSerializerSettings() 
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        var content = JsonConvert.SerializeObject(model, jsonSettings);

		return new StringContent(
             content,
             Encoding.UTF8,
             "application/json"
         );
    } 
}
