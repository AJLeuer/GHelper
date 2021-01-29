using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using NodaTime.Text;

namespace GHelperLogic.Utility.JSONConverter
{
	public class DateTimeJSONConverter : JsonConverter<LocalDateTime>
	{
		private const  string                GHubStandardDateTimeFormat = "yyyyMMdd'T'HHmmss.FFFFFF";
		private static LocalDateTimePattern  GHUBDateTimePattern        = LocalDateTimePattern.Create(GHubStandardDateTimeFormat, CultureInfo.CurrentCulture);

		public override void WriteJson(JsonWriter writer, LocalDateTime value, JsonSerializer serializer)
		{
			JToken instantJSON = GHUBDateTimePattern.Format(value);
			instantJSON.WriteTo(writer);
		}

		public override LocalDateTime ReadJson(JsonReader reader, Type objectType, LocalDateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.Value == null)
			{
				return new LocalDateTime();
			}
			return GHUBDateTimePattern.Parse(reader.Value.ToString()!).GetValueOrThrow();
		}
	}
}