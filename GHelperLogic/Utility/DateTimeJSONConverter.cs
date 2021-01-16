using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using NodaTime.Text;

namespace GHelperLogic.Utility
{
	public class DateTimeJSONConverter : JsonConverter<LocalDateTime>
	{
		public override void WriteJson(JsonWriter writer, LocalDateTime value, JsonSerializer serializer)
		{
			JToken instantJSON = value.ToString();
			instantJSON.WriteTo(writer);
		}

		public override LocalDateTime ReadJson(JsonReader reader, Type objectType, LocalDateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var gHUBDateTimePattern = LocalDateTimePattern.Create("yyyyMMdd'T'HHmmss.FFFFFF", CultureInfo.CurrentCulture);
			
			return reader.Value != null 
				? gHUBDateTimePattern.Parse(reader.Value.ToString()!).GetValueOrThrow() 
				: throw new ArgumentException("Could not parse JSON for timestamp.");
		}
	}
}