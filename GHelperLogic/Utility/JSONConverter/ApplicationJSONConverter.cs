using System;
using System.Collections.ObjectModel;
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

		public override void WriteJson(JsonWriter writer, Application? value, JsonSerializer serializer) { }

		public override Application ReadJson(JsonReader reader, Type objectType, Application? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			//storing Application converters to avoid infinite recursion
			Collection<JsonConverter<Application>> storedConverters = serializer.Converters.Store<Application>();
			Application application = serializer.Deserialize<Application>(reader)!;
			serializer.Converters.Replace(storedConverters);
			application = DetermineApplicationType(application);

			return application;
		}

		private static Application DetermineApplicationType(Application application)
		{
			if (application.Name == DesktopApplication.DesktopApplicationDefaultName)
			{
				application = new DesktopApplication(application);
			}
			else if (application.IsCustom == true)
			{
				application = new CustomApplication(application);
			}

			return application;
		}
	}
}