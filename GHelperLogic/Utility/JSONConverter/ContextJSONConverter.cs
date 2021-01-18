using System;
using GHelperLogic.Model;
using Newtonsoft.Json;

namespace GHelperLogic.Utility.JSONConverter
{
	public class ContextJSONConverter : JsonConverter<Context>
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, Context? value, JsonSerializer serializer) { }

		public override Context ReadJson(JsonReader reader, Type objectType, Context? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			serializer.Converters.Clear();
			Context context = serializer.Deserialize<Context>(reader)!;
			return context;
		}
	}
}