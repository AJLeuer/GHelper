using System;
using System.Collections.ObjectModel;
using GHelperLogic.Model;
using Newtonsoft.Json;

namespace GHelperLogic.Utility.JSONConverter
{
	public class ProfileJSONConverter : JsonConverter<Profile>
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, Profile? value, JsonSerializer serializer) { }
		
		public override Profile ReadJson(JsonReader reader, Type objectType, Profile? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			//storing Profile converters to avoid infinite recursion
			Collection<JsonConverter<Profile>> storedConverters = serializer.Converters.Store<Profile>();
			Profile profile = serializer.Deserialize<Profile>(reader)!;
			serializer.Converters.Replace(storedConverters);
			if (profile.Name == DefaultProfile.DefaultProfileDefaultName)
			{
				profile = new DefaultProfile(profile);
			}
			return profile;
		}

	}
}