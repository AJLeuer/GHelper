using System;
using NDepend.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.Utility.JSONConverter
{
	public class PathJSONConverter : JsonConverter<IPath>
	{
		public override void WriteJson(JsonWriter writer, IPath? value, JsonSerializer serializer)
		{
			JToken pathJSON = value!.ToString();
			pathJSON.WriteTo(writer);
		}

		public override IPath ReadJson(JsonReader reader, Type objectType, IPath? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			return reader.Value != null 
				? PathHelpers.ToAbsoluteFilePath(reader.Value.ToString()) 
				: throw new ArgumentException("Could not parse JSON for file path.");
		}
	}
}