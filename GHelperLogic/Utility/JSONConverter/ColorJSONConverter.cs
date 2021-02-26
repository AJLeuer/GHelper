using System;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.Utility.JSONConverter
{
	public class ColorJSONConverter : JsonConverter<Color>
	{
		public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
		{
			JToken colorJSON = ColorTranslator.ToHtml(value);
			colorJSON.WriteTo(writer);
		}

		public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.Value?.ToString() is {} colorJSON)
			{
				return ColorTranslator.FromHtml(colorJSON);
			}

			return default;
		}
	}
}