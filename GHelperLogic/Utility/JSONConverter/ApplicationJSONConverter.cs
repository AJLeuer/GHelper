using System;
using GHelperLogic.Model;
using Newtonsoft.Json;

namespace GHelperLogic.Utility.JSONConverter
{
	public class ApplicationJSONConverter : JsonConverter<Application>
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, Application? value, JsonSerializer serializer)
		{
			//note for later implementation: only include the base64 serialized poster data from the
			//application's Poster property if its IsCustom is true. We've cheated a little bit by storing
			//the image data of the poster in this property no matter where it comes from, but in G Hub
			//image data is only stored in the serialized JSON 'poster' field when 'isCustom' is true,
			//otherwise it merely relies on grabbing the poster from the URL given in the 'posterUrl' field
		}

		public override Application ReadJson(JsonReader reader, Type objectType, Application? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			serializer.Converters.Clear();
			Application application = serializer.Deserialize<Application>(reader)!;
			application = DetermineApplicationType(application);

			return application;
		}

		private static Application DetermineApplicationType(Application application)
		{
			if (application.Name == DesktopApplication.DesktopApplicationDefaultName)
			{
				application = new DesktopApplication(application);
			}

			return application;
		}
	}
}