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
			if (reader.Value == null)
			{
				return null!;
			}
			return PathHelpers.ToAbsoluteFilePath(reader.Value.ToString()) ;
		}
	}
}