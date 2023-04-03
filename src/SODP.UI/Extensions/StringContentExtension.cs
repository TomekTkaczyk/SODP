using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions;

public static class StringContentExtension
{
	public static StringContent GetRequestContent<T>(this StringContent content, T obj)
	{
		return new StringContent(
				  JsonSerializer.Serialize(obj),
				  Encoding.UTF8,
				  "application/json"
			  );
	}
}
