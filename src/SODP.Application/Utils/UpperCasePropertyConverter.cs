using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SODP.Application.Utils;

public class UpperCasePropertyConverter : JsonConverter
{
	public override bool CanConvert(Type objectType) => objectType == typeof(string);	

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		var jObject = JObject.Load(reader);

		foreach (var property in jObject.Properties())
		{
			if (property.Value.Type == JTokenType.String)
			{
				property.Value = new JValue(((string)property.Value).ToUpper());
			}
		}

		return jObject.ToObject(objectType);
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		throw new NotImplementedException();
	}
}

//public class UpperCasePropertyConverter : JsonConverter<CreateStageRequest>
//{
//	public override CreateStageRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//	{
//		using JsonDocument doc = JsonDocument.ParseValue(ref reader);
//		var root = doc.RootElement;
//		var sign = root.GetProperty(nameof(CreateStageRequest.Sign)).GetString()?.ToUpper();
//		var title = root.GetProperty(nameof(CreateStageRequest.Title)).GetString()?.ToUpper();

//		return new CreateStageRequest(sign, title);
//	}

//	public override void Write(Utf8JsonWriter writer, CreateStageRequest value, JsonSerializerOptions options)
//	{
//		writer.WriteStartObject();
//		writer.WriteString(nameof(CreateStageRequest.Sign), value.Sign);
//		writer.WriteString(nameof(CreateStageRequest.Title), value.Title);
//		writer.WriteEndObject();
//	}
//}
