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

		public override void WriteJson(JsonWriter writer, Context? value, JsonSerializer serializer)
		{
			//note for later implementation: only include the base64 serialized poster data from the
			//context's Poster property if its IsCustom is true. We've cheated a little bit by storing
			//the image data of the poster in this property no matter where it comes from, but in G Hub
			//image data is only stored in the serialized JSON 'poster' field when 'isCustom' is true,
			//otherwise it merely relies on grabbing the poster from the URL given in the 'posterUrl' field
		}

		public override Context ReadJson(JsonReader reader, Type objectType, Context? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			serializer.Converters.Clear();
			Context context = serializer.Deserialize<Context>(reader)!;
			context = DetermineContextType(context);

			return context;
		}

		private static Context DetermineContextType(Context context)
		{
			if (context.Name == DesktopContext.DesktopContextDefaultName)
			{
				context = new DesktopContext(context);
			}

			return context;
		}
	}
}