using Newtonsoft.Json;
using System;
using System.Globalization;


namespace SODP.Shared.JSON;

public class CustomDateOnlyConverter : JsonConverter<DateOnly?>
{
	private const string _dateFormat = "yyyy-MM-dd";

	public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.String)
		{
			string dateStr = (string)reader.Value;
			if (DateTime.TryParseExact(dateStr, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
			{
				return DateOnly.FromDateTime(parsedDate);
			}
		}

		return null;
	}

	public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
	{
		if (value.HasValue)
		{
			writer.WriteValue(value.Value.ToString(_dateFormat));
		}
		else
		{
			writer.WriteNull();
		}
	}
}
